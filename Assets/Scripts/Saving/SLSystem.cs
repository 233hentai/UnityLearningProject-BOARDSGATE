using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BOARDSGATE.Saving{
    public class SLSystem : MonoBehaviour
    {
        public void Save(string fileName){
            string path=GetPathWithFileName(fileName);
            print("Saving to: "+path);
            using(FileStream stream=File.Open(path,FileMode.Create)){
                BinaryFormatter formatter=new BinaryFormatter();
                formatter.Serialize(stream,GetStates());
                // byte[] buffer=SerializeVector(playerTransform.position);
                // stream.Write(buffer,0,buffer.Length);
            }
        }

        public void Load(string fileName){
            string path=GetPathWithFileName(fileName);
            print("Loading from: "+GetPathWithFileName(fileName));
            using(FileStream stream=File.Open(path,FileMode.Open)){
                // byte[] buffer=new byte[stream.Length];
                // stream.Read(buffer,0,buffer.Length);
                BinaryFormatter formatter=new BinaryFormatter();
                RestoreStates(formatter.Deserialize(stream));

            }
        }

        private object GetStates()
        {
            Dictionary<string,object> states=new Dictionary<string, object>();
            foreach(EntitySavable entity in FindObjectsOfType<EntitySavable>()){
                states[entity.GetUniqueID()]=entity.GetStates();
            }
            return states;
        }

        private void RestoreStates(object states)
        {
            Dictionary<string,object> dictionary=(Dictionary<string,object>)states;
            foreach (EntitySavable entity in FindObjectsOfType<EntitySavable>())
            {
                entity.RestoreStates(dictionary[entity.GetUniqueID()]);
            }
        }

        private string GetPathWithFileName(string fileName){
            return Path.Combine(Application.persistentDataPath,fileName+".sav");
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


