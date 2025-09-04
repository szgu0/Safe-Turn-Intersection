using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class truckcamera : MonoBehaviour
{
    // public float distance;
    public GameObject followCam;
    public GameObject driveCam;
    public GameObject outlookCam;

  public GameObject truck_DrivePoint;
  public GameObject truck_DriveLookPoint;
  public GameObject truck_FollowPoint;


    
    public GameObject truck;

    [Header("ChangePosition")]
    public float follow_x_value;
    public float follow_z_value;
    public float follow_y_value;

    public float DrivePoint_x_value;
    public float DrivePoint_y_value;
    public float DrivePoint_z_value;

    public float DriveLookPoint_x_value;
    public float DriveLookPoint_y_value;
    public float DriveLookPoint_z_value;

    public float Bord_x_value;
    public float Bord_y_value;
    public float Bord_z_value;

    public float Bord_x1_value;
    public float Bord_y1_value;
    public float Bord_z1_value;

    public GameObject bord;
    public bool hasFollowPointChanged = false;
    public bool hasDrivePointChanged = false;


    private Vector3 initialFollowPointPosition; 
    private Vector3 initialDrivePointPosition;
    private Vector3 initialDriveLookPointPosition;
    private Vector3 initialBordPosition; 

    // Start is called before the first frame update
    void Start()
    {
        initialFollowPointPosition = truck_FollowPoint.transform.position;
        initialDrivePointPosition=truck_DrivePoint.transform.position;
        initialDriveLookPointPosition=truck_DriveLookPoint.transform.position;
        initialBordPosition = bord.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       // truck_DrivePoint.transform.position = drivepoint;  
        //drivelookpoint = truck_DriveLookPoint.transform.position;
      

        if (truck.activeInHierarchy && followCam.activeInHierarchy && !hasFollowPointChanged)
        {

            ChangeFollowCam();
        }
       else if (!(truck.activeInHierarchy && followCam.activeInHierarchy && !hasFollowPointChanged))
        {
            
            return;
        }
        if (truck.activeInHierarchy && driveCam.activeInHierarchy && !hasDrivePointChanged)
        {

            ChangeDriveCam();
        }
       else if (!(truck.activeInHierarchy && driveCam.activeInHierarchy && !hasDrivePointChanged))
        {
            return;
        }

        if (outlookCam.activeInHierarchy)
        {
            hasDrivePointChanged=false;
            hasFollowPointChanged=false;
            truck_FollowPoint.transform.position = initialFollowPointPosition;
            truck_DrivePoint.transform.position=initialDrivePointPosition;
            truck_DriveLookPoint.transform.position=initialDriveLookPointPosition;
        }

    }
   public void ChangeFollowCam()
    {
        truck_FollowPoint.transform.position = new Vector3(truck_FollowPoint.transform.position.x-follow_x_value, truck_FollowPoint.transform.position.y+ follow_y_value, truck_FollowPoint.transform.position.z-follow_z_value);
        hasFollowPointChanged = true;

        bord.transform.position = new Vector3(bord.transform.position.x - Bord_x_value, bord.transform.position.y + Bord_y_value, bord.transform.position.z - Bord_z_value);

    }
   public void ChangeDriveCam()
    {
        bord.transform.position = new Vector3(bord.transform.position.x -Bord_x1_value, bord.transform.position.y +Bord_y1_value, bord.transform.position.z + Bord_z1_value);

        truck_DriveLookPoint.transform.position = new Vector3(truck_DriveLookPoint.transform.position.x + DriveLookPoint_x_value, truck_DriveLookPoint.transform.position.y + DriveLookPoint_y_value, truck_DriveLookPoint.transform.position.z + DriveLookPoint_z_value);

        truck_DrivePoint.transform.position = new Vector3(truck_DrivePoint.transform.position.x + DrivePoint_x_value, truck_DrivePoint.transform.position.y + DrivePoint_y_value, truck_DrivePoint.transform.position.z + DrivePoint_z_value);

        hasDrivePointChanged = true;
    }

}
