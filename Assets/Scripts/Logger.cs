using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFirebaseUnity;

public class Logger : MonoBehaviour {

    Firebase firebase;
    BasketballTFData previousData;

    void Start()
    {
        firebase = Firebase.CreateNew("https://basketball-tf.firebaseio.com/");
        LogData(Time.time.ToString(), System.DateTime.Now.ToString(), 0, 0, 0.0);
    }

    public void LogData(string timestampGame, string timestamp, long shotsTotal, long shotsSuccessful, double shotsSuccessfulPercentag)
    {
        BasketballTFData logData;
        if(previousData != null)
        {
            long ShotsSinceLastLog = shotsTotal - previousData.ShotsTotal;
            long ShotsSuccessfulSinceLastLog = shotsSuccessful - previousData.ShotsSuccessful;
         //    ShotsSinceLastLogSuccessfulPercentag = 0.0;
           
            double  ShotsSinceLastLogSuccessfulPercentag = (ShotsSuccessfulSinceLastLog / ShotsSinceLastLog );



            logData = new BasketballTFData(timestampGame, timestamp, shotsTotal, shotsSuccessful, shotsSuccessfulPercentag, 
                                               ShotsSinceLastLog,
                                               ShotsSuccessfulSinceLastLog,
                                               ShotsSinceLastLogSuccessfulPercentag
                                              );
        } else {
            logData = new BasketballTFData(timestampGame, timestamp, shotsTotal, shotsSuccessful, shotsSuccessfulPercentag,
                                               0, 0, 0.0
                                              );
        }

        previousData = logData;

        firebase.Child(SystemInfo.deviceName).Push(JsonUtility.ToJson(logData), true);

        firebase.OnDeleteSuccess += (Firebase sender, DataSnapshot snapshot) => {
            Debug.Log("[OK] Delete from " + sender.Endpoint + ": " + snapshot.RawJson);
        };

        firebase.OnUpdateFailed += UpdateFailedHandler;
        // Method signature: void UpdateFailedHandler(Firebase sender, FirebaseError err)

        firebase.GetValue("print=pretty");
    }
    void UpdateFailedHandler(Firebase sender, FirebaseError err){
        Debug.Log(err);
    }
	// Update is called once per frame
	void Update () {
        
        if(Time.frameCount%300 == 0){
            LogData(Time.time.ToString(),
                    System.DateTime.Now.ToString(),
                    BallController.ShotCount,
                    BallController.SuccessCount,
                    BallController.SuccessCount / (float)BallController.ShotCount * 100f
                   );
        }
	}
}

public class BasketballTFData {
    public string TimestampGame;
    public string Timestamp;
    public long ShotsTotal;
    public long ShotsSuccessful;
    public double ShotsSuccessfulPercentag;
    public long ShotsSinceLastLog;
    public long ShotsSuccessfulSinceLastLog;
    public double ShotsSinceLastLogSuccessfulPercentag;

    public BasketballTFData(string timestampGame, string timestamp, long shotsTotal, long shotsSuccessful, double shotsSuccessfulPercentag, long shotsSinceLastLog, long shotsSuccessfulSinceLastLog, double shotsSinceLastLogSuccessfulPercentag)
    {
        TimestampGame = timestampGame;
        Timestamp = timestamp;
        ShotsTotal = shotsTotal;
        ShotsSuccessful = shotsSuccessful;
        ShotsSuccessfulPercentag = shotsSuccessfulPercentag;
        ShotsSinceLastLog = shotsSinceLastLog;
        ShotsSuccessfulSinceLastLog = shotsSuccessfulSinceLastLog;
        ShotsSinceLastLogSuccessfulPercentag = shotsSinceLastLogSuccessfulPercentag;
    }
}
