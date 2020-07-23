using System.Collections;
using System.Collections.Generic;

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Linq;
using System.Threading;

using UnityEngine;
using UnityEngine.SceneManagement;

using TouchControlsKit;

public class UploadScript : MonoBehaviour
{
    public string ftpHost = "ftp://localhost";
    public string ftpUsername = "testuser";
    public string ftpPassword = "testpass"; 

    private bool uploadSuccessfull = false;

    // Start is called before the first frame update
    void Start()
    {
        string id = SystemInfo.deviceUniqueIdentifier;
        string folder = Application.persistentDataPath;

        string path = CompressData(folder, id);

        int maxRetries = 3;
        int ret = 0;
        
        ret = Upload(path);
        if(ret != 1)
        {
            for(int i=0; i < maxRetries; i++)
            {
                ret = Upload(path);
                if(ret == 1)
                    break;
                Debug.Log("Retries: " + (i+1));
                Thread.Sleep(500);
            }
        }

        uploadSuccessfull = true;

        if(uploadSuccessfull)
            Debug.Log("Thanks");
    }

    // Update is called once per frame
    void Update()
    {
        if (TCKInput.GetAction("ButtonOK", EActionEvent.Click))
        {
            if(uploadSuccessfull)
            {
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }
    }

    int Upload(string fullPath)
    {
        string[] split = fullPath.Split('/');
        string filename = split[split.Length - 1];

        string responseDescription = null;
        FtpStatusCode responseCode = 0;

        try
        {
            FtpWebRequest request = (FtpWebRequest) WebRequest.Create(ftpHost + "/" + filename);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            byte[] fileContents = File.ReadAllBytes(fullPath);
            request.ContentLength = fileContents.Length;

            using(Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }
            
            using(FtpWebResponse response = (FtpWebResponse) request.GetResponse())
            {
                responseDescription = response.StatusDescription;
                responseCode = response.StatusCode;
                //Debug.Log(responseCode.ToString());
                //Debug.Log(responseDescription);
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
            return 0;
        }

        if(responseCode == FtpStatusCode.ClosingControl || responseCode == FtpStatusCode.ClosingData)
        {
            return 1;
        }

        return 0;
    }

    string CompressData(string folder, string id)
    {
        string zipPath = folder + "/" + id + ".zip";
        string prefix = "D3x_";
        string infix = System.DateTime.UtcNow.ToString("yy-MM-dd-HH-mm-ss") + (char)UnityEngine.Random.Range(65, 122);
        string postfix = "_data.zip";

        try{
            if(File.Exists(zipPath))
                File.Delete(zipPath);
        }
        catch(Exception e)
        {
            Debug.Log("(File.Exists) Error: " + e.Message);
        }

        try{
            ZipFile.CreateFromDirectory(folder + "/" + id, folder + "/" + id + ".zip");

            // Rename zip file
            System.IO.File.Move(folder + "/" + id + ".zip", folder + "/" + prefix + infix + postfix);
        }
        catch(Exception e)
        {
            Debug.Log("(ZipFile.CreateFromDirectory) Error: " + e.Message);
            return null;
        }

        // return folder + ".zip";
        return folder + "/" + prefix + infix + postfix;
    }
}
