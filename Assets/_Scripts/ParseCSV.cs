using UnityEngine;
using System.Collections;
using System.IO;
using System;
public class CSVParsing
{
    public TextAsset csvFile; // Reference of CSV file
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter

    private int numCol;
    public CSVParsing(TextAsset csvFileIn, int numColIn)
    {
        csvFile = csvFileIn;
        numCol = numColIn;
    }


    // Read data from CSV file
	// TODO: Pass in an array to write to, return an array, or something...
    public string[,] readData()
    {
        string[] records = csvFile.text.Split(lineSeperater);
        string[,] output = new string[records.Length-1, numCol];

        // Last row is empty due to newline at bottom of csv (so ... -1)
        for (int i = 0; i < records.Length-1; i++ )
        {
            string record = records[i];
            string[] fields = record.Split(fieldSeperator);
            
            for (int j = 0; j < fields.Length; j++ )
            {

                if (fields.Length != numCol){

                    throw new Exception(String.Format("# of fields in CSV didn't match expected # of columns. Expected: {0}, found: {1}", numCol, fields.Length));
                    continue;
                }
                output[i,j] = fields[j];
                // Debug.Log(field);
				// contentArea.text += field + "\t";
            }
			// Debug.Log("\n");
            // contentArea.text += '\n';
        }
        return output;
    }
    // Get path for given CSV file
    private static string getPath()
    {
		#if UNITY_EDITOR
				return Application.dataPath;
		#elif UNITY_ANDROID
		return Application.persistentDataPath;// +fileName;
		#elif UNITY_IPHONE
		return GetiPhoneDocumentsPath();// +"/"+fileName;
		#else
		return Application.dataPath;// +"/"+ fileName;
		#endif
    }
	
    // Get the path in iOS device
    private static string GetiPhoneDocumentsPath()
    {
        string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
        path = path.Substring(0, path.LastIndexOf('/'));
        return path + "/Documents";
    }
}