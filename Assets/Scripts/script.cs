using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class script : MonoBehaviour {

	// Use this for initialization
	//the menu text
	public Text game1;
	public Text game2;
	public Text game3;
	public Text game4;
	public GameObject help_button;
	public GameObject help_panel;
	public GameObject welcome_panel;
	public GameObject rating_panel;
	public GameObject difficulty_panel;
	public Text rating_text;
	//the array for the names of the games and the order in which they will laod
	String[] gameslist;
	int[] gamesOrder;

	//the variables
	public static String gamename="";
	string filename="";
	string score;
	float duration;
	float selectiontime;

	Boolean returning=false; //stores whether the user is new or returning
	int enjoyment;
	FileStream sessionFile;
	String filetext="";
	String path;
	void Start () {
		
		gameslist=new string[]{"Sort the Court","Mr Blue","90 Second Portriats","SuperSBugs"};
		gamesOrder = gamesOrdering();
		duration = 0.0f;
		selectiontime = Time.time;
		intialiseMenu ();
		path = Application.dataPath + "/Datafiles/";

		System.Random rnd = new System.Random ();
		int temp = rnd.Next(1, 10000000);
		filename="session "+ DateTime.Now.Date.Month+" "+ DateTime.Now.Date.Day+" "+DateTime.Now.Date.Hour+" "+DateTime.Now.Minute+" "+temp +".txt";


		filetext+="Game Session\n Date: "+ DateTime.Now.Date.Month+" "+ DateTime.Now.Date.Day+"\nTime: hr"+DateTime.Now.Date.Hour+" min"+DateTime.Now.Minute;
		filetext += "\nReturning: " +returning;


	}

	public void onGame(int game){

			
			//game1.text = gameslist [gamesOrder [0]];
			switch(game){
		case 1: 
				//print(game1.text);
			gamename = game1.text;

				break;
			case 2: 
				//print(game2.text);
				gamename=game2.text;
				break;

			case 3: 
	
				gamename=game3.text;
				break;
			case 4: 

				gamename=game4.text;
				break;
			
	
		}
			
		filetext+="\nGame Selected: "+gamename+"\nselectiontime: "+(Time.time-selectiontime);
		rating_text.text = "You just played " + gamename;
		rating_panel.SetActive (true);
		selectiontime = Time.time;
		duration = Time.time;//the time in which you selected the game
			// OPEN THE FILE
		startGame();
		
			
		
	}

	void startGame(){
		//when the game starts
		selectiontime = Time.time;
		if (filename == "" || gamename == "") {
			return;
		} 
			try {
				Process myprocess = new Process ();
				myprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				myprocess.StartInfo.CreateNoWindow = true;
				myprocess.StartInfo.UseShellExecute = false;
			myprocess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";//open the windows terminal
				
			string path = Application.dataPath; //get systems path to assets folder ie
			// "C:\\Users\\Mosadi\\Documents\\Projects\\game_menu\\Assets";
			path= path.Substring(0,path.IndexOf("/Assets"));//remove the /assets, you canno place other games in the assets folder
			//the path depends on the gamename
			if(gamename.Equals("Superbugs"))
			path+= "\\Superbugs.exe";//check which game was chosen, the set itto path
			if(gamename.Equals("90 Seconds Portriats"))
				path= "Superbugs.exe";//only superbugs works for now, the links to the other games are in games are in the gameslist.pdf
			if(gamename.Equals("Mr. Blue"))
				path+= "\\Superbugs.exe";//would be great if we could do this in linux
			if(gamename.Equals("Sort the court"))
				path= "\\Superbugs.exe";
			  print(path);
			myprocess.StartInfo.Arguments = "/c" + path;//this is required in windows
				myprocess.EnableRaisingEvents = true;
				//Process.Start ("mono", "path");
				myprocess.Start ();
				myprocess.WaitForExit ();
				int exitCode = myprocess.ExitCode;

				//print(exitCode);
			} catch (Exception e) {
				print (e);

		}
	}
	public void onRateGame(int rating){
		filetext+="\nGame duration: "+(Time.time-duration) +"Seconds";
		filetext+="\nGame Rating(0 if you liked it): "+rating;
		rating_panel.SetActive (false);
		difficulty_panel.SetActive (true);

	}
	public void onDifficulty(int rating){
		
		filetext+="\nGame Difficulty(1-easy,2-medium,3-hard): "+rating;
		difficulty_panel.SetActive (false);

	}

	public void onHelp(){
		help_panel.SetActive (true);
		help_button.SetActive (false);
		filetext += "\nRequested help " +(Time.time-selectiontime);
		selectiontime = Time.time;
	}

	public void exitHelp(){
		help_panel.SetActive (false);
		help_button.SetActive (true);
		selectiontime = Time.time;
	}

	public void onLoad(int player){
		if (player == 2) {
			returning = true; //2 means you are returning
		} else {
			returning = false;// 1 means you are new
		}
		welcome_panel.SetActive (false);
		selectiontime = Time.time;
	}
	public void exitGame(){
		if (File.Exists(filename))
		{
			//Check if this file exits

			File.Delete(filename);
		}

		// Create the file.
		sessionFile = File.Create(path+filename);
		filetext += "\nExited After: " + Time.time+" seconds";
		
		using (sessionFile)
		{
			Byte[] info = new UTF8Encoding(true).GetBytes(filetext);
			// WRITE TO THE FILE
			sessionFile.Write(info, 0, info.Length);
		}
		sessionFile.Close ();
		//print (filetext);
		SendEmail ();
		Application.Quit ();
	}
	// Update is called once per frame
	void Update () {
		
	}
	 int[] gamesOrdering(){
		int [] list= new int[]{0,1,2,3};
		System.Random rnd = new System.Random();
		int temp = rnd.Next(1, 10);
		if (temp > 5) {
			temp = list [0];
			list [0] = list [3];
			list [3] = temp;
		} else {
			temp = list [1];
			list [1] = list [2];
			list [2] = temp;
			
		}
		temp = rnd.Next(1, 20);
		if (temp > 9) {
			temp = list [0];
			list [0] = list [2];
			list [2] = temp;
		} else {
			temp = list [3];
			list [3] = list [1];
			list [1] = temp;

		}
		temp = rnd.Next(1, 20);
		if (temp < 9) {
			temp = list [1];
			list [1] = list [2];
			list [2] = temp;
		} else {
			temp = list [3];
			list [3] = list [0];
			list [0] = temp;

		}
		//print(list[0]+" and " +list[1]+" and " +list[2]+" and " +list[3]+" it does the things " );
		return list;
	}
	void intialiseMenu(){
		game1.text = gameslist [gamesOrder [0]];
		game2.text = gameslist [gamesOrder [1]];
		game3.text = gameslist [gamesOrder [2]];
		game4.text = gameslist [gamesOrder [3]];
	}
	void SendEmail ()
	{
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("gamesstatictics@gmail.com");
		mail.To.Add("gamesstatictics@gmail.com");
		mail.To.Add("kgatle.regina@gmail.com");
		mail.Subject = "GAMES SELECTION";
		mail.Body = filetext;
		// string filename = "screenshot.png";
		mail.Attachments.Add (new Attachment(path+ filename));
		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("gamesstatictics@gmail.com", "67Gam3sStats") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };
		smtpServer.Send(mail);
		print("Email Was successfully sent");

	}
}
