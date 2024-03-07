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
                Transform playerTransform=GetPlayerTransform();
                Vector3Serializable position=new Vector3Serializable(playerTransform.position);
                BinaryFormatter formatter=new BinaryFormatter();
                formatter.Serialize(stream,position);
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
                Vector3Serializable position=(Vector3Serializable)formatter.Deserialize(stream);
                Transform playerTransform=GetPlayerTransform();
                playerTransform.position=position.ToVector3();

            }
        }

        private string GetPathWithFileName(string fileName){
            return Path.Combine(Application.persistentDataPath,fileName+".sav");
        }

        private byte[] SerializeVector(Vector3 vector){
            byte[] buffer=new byte[4*3];
            BitConverter.GetBytes(vector.x).CopyTo(buffer,0);
            BitConverter.GetBytes(vector.y).CopyTo(buffer,4);
            BitConverter.GetBytes(vector.z).CopyTo(buffer,8);
            return buffer;
        }

        private Vector3 DeserializeVector(byte[] buffer){
            Vector3 vector;
            vector.x=BitConverter.ToSingle(buffer,0);
            vector.y=BitConverter.ToSingle(buffer,4);
            vector.z=BitConverter.ToSingle(buffer,8);
            return vector;
        }

        private Transform GetPlayerTransform(){
            return GameObject.FindWithTag("Player").transform;
        }

    }
}


