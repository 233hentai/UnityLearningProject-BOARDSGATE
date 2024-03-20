using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BOARDSGATE.Saving
{
    public class SLSystem : MonoBehaviour
    {
        public void Save(string fileName){
            Dictionary<string, object> states=LoadFile(fileName);
            GetStates(states);
            SaveFile(fileName,states);
        }

        public void Load(string fileName){            
            RestoreStates(LoadFile(fileName));
        }

        private void SaveFile(string fileName, object states)
        {
            string path=GetPathWithFileName(fileName);
            print("Saving to: "+path);
            using(FileStream stream=File.Open(path,FileMode.Create)){
                BinaryFormatter formatter=new BinaryFormatter();
                formatter.Serialize(stream,states);
            }
        }
        private Dictionary<string, object> LoadFile(string fileName)
        {
            string path=GetPathWithFileName(fileName);
            if(!File.Exists(path)){
                return new Dictionary<string, object>();
            }
            print("Loading from: "+GetPathWithFileName(fileName));
            using(FileStream stream=File.Open(path,FileMode.Open)){
                BinaryFormatter formatter=new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void GetStates(Dictionary<string,object> states)
        {
            foreach(EntitySavable entity in FindObjectsOfType<EntitySavable>()){
                string UID=entity.GetUniqueID();
                states[UID]=entity.GetStates();
            }
            states["LastSceneIndex"]=SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreStates(Dictionary<string, object> states)
        {
            foreach (EntitySavable entity in FindObjectsOfType<EntitySavable>())
            {
                if(states.ContainsKey(entity.GetUniqueID())){
                    entity.RestoreStates(states[entity.GetUniqueID()]);
                }
            }
        }

        private string GetPathWithFileName(string fileName){
            return Path.Combine(Application.persistentDataPath,fileName+".sav");
        }

        public IEnumerator LoadLastScene(string savingFileName){
            Dictionary<string,object> states=LoadFile(savingFileName);
            if(states.ContainsKey("LastSceneIndex")){
                int index=(int)states["LastSceneIndex"];
                if(index!=SceneManager.GetActiveScene().buildIndex){
                    yield return SceneManager.LoadSceneAsync(index);
                }
            }
            RestoreStates(states);
        }

        // private byte[] SerializeVector(Vector3 vector){
        //     byte[] buffer=new byte[4*3];
        //     BitConverter.GetBytes(vector.x).CopyTo(buffer,0);
        //     BitConverter.GetBytes(vector.y).CopyTo(buffer,4);
        //     BitConverter.GetBytes(vector.z).CopyTo(buffer,8);
        //     return buffer;
        // }

        // private Vector3 DeserializeVector(byte[] buffer){
        //     Vector3 vector;
        //     vector.x=BitConverter.ToSingle(buffer,0);
        //     vector.y=BitConverter.ToSingle(buffer,4);
        //     vector.z=BitConverter.ToSingle(buffer,8);
        //     return vector;
        // }

    }
}


