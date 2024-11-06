// Tutorial used for patrolling state: https://www.youtube.com/watch?v=vS6lyX2QidE&t=238s


using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Enemy_Movement : MonoBehaviour
{
    private NavMeshAgent enemy_Nav_Agent; 
    private GameObject player_Object;
    private GameObject search_Point = null;
    
    // For if I reuse and don't hardcode
    //public String[] enemy_Animator_Bool;
    [Header("General")] 
    [SerializeField] private Animator enemy_Animator;
    [SerializeField] private Enemy_Movement current_Script;
    [SerializeField] private Enemy_FOV enemy_FOV_Script;
    [SerializeField] private Enemy_Status enemy_Status_Script;
    [SerializeField] private GameObject enemy_View_Rotator; // Object to manipulate rig and have enemy properly aim at player
    [SerializeField] private GameObject enemy_Rig_Spine; // cant child spine to enemy_View_Rotator, this is a work around
    
    [Header("Patrol Points - Where the enemy will move towards")]
    [SerializeField] private Transform[] patrol_Points; // Array of patrol waypoints, added in editor
    
    private enum State
    {
        // Patrol between set points
        Patrol,
        // Hunt player when in sight + go to last known location
        Chase_Player,
        // When player first goes out of sight, enemy will do a short search
        Search_For_Player,
        // When enemy reaches player
        Attack_Player,
        // Standing between walking to patrol points
        Patrol_Idle,
        
        Death
    }
    
    private State current_State = State.Patrol;
    private int previous_State = 1;
    private bool search_Point_Placed;
    
    [Header("Patrol Options")]
    [SerializeField] private float patrol_Speed;
    [SerializeField] private float patrol_Idle_Time; // Time spent at patrol point before moving
    private int target_Point; // Next patrol point to walk to
    private float patrol_Timer; 
    
    [Header("Chase Options")]
    public float chase_Speed;   
    
    [Header("Search Options")]
    [SerializeField] private int wander_Amount; // How many times the enemy will wander before going back to patrol
    [SerializeField] private float wander_Duration;
    private int wander_Inc = 0; // Increment for wander_Amount;
    private float search_Timer;

    [Header("Attack Options")] 
    [SerializeField] private Enemy_Shooting shooting_Script;
    [SerializeField] private float attack_Cooldown;
    [SerializeField] private float attack_Range; // Range of firing at player, not range of bullets 
    private bool can_Shoot;

    [HideInInspector] public bool has_Died = false;
    
    private void Start()
    {
        enemy_Nav_Agent = GetComponent<NavMeshAgent>();
        player_Object = GameObject.FindWithTag("Player");
        target_Point = 0;
        search_Point_Placed = false;
        enemy_Animator.SetBool("Walking State", true);
        enemy_Nav_Agent.speed = patrol_Speed;
        can_Shoot = true;
    }// end Start

    
    private void Update()
    {
        //print(current_State);
        //Debug.Log(search_Point_Placed);
        switch (current_State)
        {
            case State.Patrol:
                if (enemy_Animator.GetBool("Walking State") == false)
                {
                    enemy_Nav_Agent.speed = patrol_Speed;
                    enemy_FOV_Script.FOV_Angle = enemy_FOV_Script.temp_Searching_FOV_Angle;
                    //enemy_Animator.SetBool("Searching State", false);
                    //enemy_Animator.SetBool("Chasing State", false);
                    enemy_Animator.SetBool("Idle State", false);
                    enemy_Animator.SetBool("Walking State", true);
                    enemy_Animator.SetBool("Attack State", false);
                }
                Patrol_State();
                break;
            
            case State.Attack_Player:
                if (enemy_Animator.GetBool("Attack State") == false)
                {
                    enemy_Animator.SetBool("Walking State", false);
                    //enemy_Animator.SetBool("Chasing State", false);
                    //enemy_Animator.SetBool("Searching State", false);
                    enemy_Animator.SetBool("Idle State", false);
                    enemy_Animator.SetBool("Attack State", true);
                }
                Attack_Player_State();
                break;   
            
            case State.Chase_Player:
                if (enemy_Animator.GetBool("Walking State") == false)
                {
                    enemy_Nav_Agent.speed = chase_Speed;
                    enemy_FOV_Script.FOV_Angle = enemy_FOV_Script.searching_FOV_Angle;
                    enemy_Animator.SetBool("Walking State", true);
                    //enemy_Animator.SetBool("Searching State", false);
                    enemy_Animator.SetBool("Idle State", false);
                   // enemy_Animator.SetBool("Chasing State", true);
                }
                Chase_Player_State();
                break;

            case State.Search_For_Player:
                if (enemy_Animator.GetBool("Walking State") == false)
                {
                    //enemy_Animator.SetBool("Chasing State", false);
                    enemy_Animator.SetBool("Idle State", false);
                    enemy_Animator.SetBool("Walking State", true);
                    enemy_Animator.SetBool("Attack State", false);
                } 
                
                Search_State();
                break;
            
            case State.Patrol_Idle:
                if (enemy_Animator.GetBool("Idle State") == false)
                {
                    enemy_Animator.SetBool("Walking State", false);
                    //enemy_Animator.SetBool("Chasing State", false);
                    //enemy_Animator.SetBool("Searching State", false);
                    enemy_Animator.SetBool("Idle State", true);
                    enemy_Animator.SetBool("Attack State", false);
                }   
                
                Idle_State();
                break;
            
            case State.Death:
                if (enemy_Animator.GetBool("Dying State") == false)
                {
                    enemy_Animator.SetBool("Dying State", true);
                    enemy_Animator.SetBool("Walking State", false);
                    //enemy_Animator.SetBool("Chasing State", false);
                    //enemy_Animator.SetBool("Searching State", false);
                    enemy_Animator.SetBool("Idle State", false);
                    enemy_Animator.SetBool("Attack State", false);
                }
                
                Death_State();
                break;
            
        }// end State switch
    }// end Update

    
    private void Patrol_State()
    {
        if (search_Point != null)
        {
            search_Point_Placed = false;
            //Destroy_Search_Point();
        }
        
        float distance_To_Waypoint = Vector3.Distance(patrol_Points[target_Point].position, transform.position);

        // Checks if close enough to target waypoint, then changes to next
        if (distance_To_Waypoint <= .7)
        {
            Change_State(5);
        }
        
        enemy_Nav_Agent.SetDestination(patrol_Points[target_Point].position);

    }// end Patrol_State


    private void Attack_Player_State()
    {
        if (enemy_FOV_Script.player_Visible == false)
            Change_State(3);
        
        enemy_View_Rotator.transform.LookAt(player_Object.transform.position);

        Vector3 player_Direction = player_Object.transform.position - transform.position;
        player_Direction.y = 0;

        if (player_Direction.sqrMagnitude > 0.01f)
        {
            Quaternion target_Rotation = Quaternion.LookRotation(player_Direction);
            transform.rotation = Quaternion.Euler(0, target_Rotation.eulerAngles.y, 0);
        }
        
/*
        float distance_To_Player = Vector3.Distance(gameObject.transform.position, player_Object.transform.position); 
        // check for outside of max fire range, move into range
        if (distance_To_Player > attack_Range)
        {
            // should in theory take the halfway point between self and player and move towards it
            Vector3 attack_Position = (gameObject.transform.position - player_Object.transform.position) / 2;
            Move_To_State(attack_Position);
        }// end moving into range
  */      
        if (can_Shoot)
        {
            can_Shoot = false;
            shooting_Script.Shoot();
            Invoke("Reset_Attack", attack_Cooldown);
        }
    }// end Attack_Player_State

    private void Reset_Attack()
    {
        can_Shoot = true;
    }// end Reset_Attack

    // to cancel other states when moving into position
    private void Move_To_State(Vector3 destination)
    {
        
    }// end Move_To_State
    
    private void Chase_Player_State()
    {
        if (search_Point != null)
        {
            search_Point_Placed = false;
            //Destroy_Search_Point();
        }
        
        float dist_To_Last_Known_Location = Vector3.Distance(transform.position, enemy_FOV_Script.last_Player_Location);
        
        //Debug.Log("enter chase state");
        
        if (dist_To_Last_Known_Location <= 3)
        {
            Debug.Log("reached last known");
            Change_State(3);
            return;
        }
        
        // Chases player instead of just mindlessly walking towards the first given location
        if (enemy_FOV_Script.player_Visible == true)
        {
            enemy_Nav_Agent.SetDestination(player_Object.transform.position);
        }
        else
        {
            enemy_Nav_Agent.SetDestination(enemy_FOV_Script.last_Player_Location);
        }
        
    }// end Chase_Player_State

    
    private void Search_State()
    {
        search_Timer += Time.deltaTime;
        
        if (search_Timer >= wander_Duration && wander_Inc < wander_Amount)
        {
            Debug.Log("new wander");
            Vector3 random_Point = Random.insideUnitCircle * enemy_FOV_Script.FOV_Radius;
            enemy_Nav_Agent.SetDestination(random_Point);
            search_Timer = 0;
            wander_Inc++;
        }
        else if (wander_Inc >= wander_Amount)
        {
            Debug.Log(("back to patrol"));
            wander_Inc = 0;
            Change_State(1); // go back to patrol
        }
        
    }// end Search_State
    
     private void Idle_State()
     {
         if (patrol_Timer >= patrol_Idle_Time)
         {
             patrol_Timer = 0;
             target_Point = (target_Point + 1) % patrol_Points.Length;
             Change_State(1);
         }
         else
             patrol_Timer += Time.deltaTime;
     }// end Idle_State   

     private void Death_State()
     {
         if (has_Died)
         {
             enemy_FOV_Script.enabled = false;
             enemy_Status_Script.enabled = false;
             current_Script.enabled = false;
         }
     }// end Death_State()


     
    // 1 is Patrol, 2 is Chase, 3 is Search
    public void Change_State(int new_State)
    {
        //Debug.Log(previous_State);
        if (previous_State == new_State)
            return;
        
        enemy_View_Rotator.transform.rotation = gameObject.transform.rotation;
        
        //Debug.Log($"Changing state: {current_State}");
        
        if (new_State == 1)
            current_State = State.Patrol;
        
        if (new_State == 2)
            current_State = State.Chase_Player;
        
        if (new_State == 3)
            current_State = State.Search_For_Player;

        if (new_State == 4)
            current_State = State.Attack_Player;

        if (new_State == 5)
            current_State = State.Patrol_Idle;

        if (new_State == 6)
            current_State = State.Death;
        
        previous_State = new_State;
    }// end Change_States

}// end Enemy_Movement


    