using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using BOARDSGATE.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace BOARDSGATE.Saving{
    [ExecuteAlways]
    public class EntitySavable : MonoBehaviour
    {
        [SerializeField] string uniqueID="";

        static Dictionary<string,EntitySavable> globalEntitySavable=new Dictionary<string, EntitySavable>();

#if UNITY_EDITOR//打包项目时不包括此段
        private void Update() {
            //需要在编辑界面就给场景中的实例生成GUID但同时不能给prefab生成
            if(Application.IsPlaying(this)) return;
            if(string.IsNullOrEmpty(gameObject.scene.path)) return;
            SerializedObject serializedObject=new SerializedObject(this);
            SerializedProperty property=serializedObject.FindProperty("uniqueID");
            if(string.IsNullOrEmpty(property.stringValue)||!IsUnique(property.stringValue)){
                property.stringValue=System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
            globalEntitySavable[property.stringValue]=this;
        }
#endif

        public string GetUniqueID(){
            return uniqueID;
        }

        public object GetStates(){
            Dictionary<string,object> states=new Dictionary<string, object>();
            foreach(ISaveable iSavable in GetComponents<ISaveable>()){
                states[iSavable.GetType().ToString()]=iSavable.GetStates();
            }
            return states;
        }

        public void RestoreStates(object states){
            Dictionary<string,object> statesDictionary=(Dictionary<string,object>)states;
            foreach(ISaveable iSavable in GetComponents<ISaveable>()){
                string type=iSavable.GetType().ToString();
                if(statesDictionary.ContainsKey(type)){
                    iSavable.RestoreStates(statesDictionary[type]);
                }
            }
        }

        private bool IsUnique(string UID)
        {
            if(!globalEntitySavable.ContainsKey(UID)) return true;
        
            if(globalEntitySavable[UID]==this) return true;

            if(globalEntitySavable[UID]==null){
                globalEntitySavable.Remove(UID);
                return true;
            }

            if(globalEntitySavable[UID].GetUniqueID()!=UID){
                globalEntitySavable.Remove(UID);
                return true;
            }

            return false;
        }
    }
}

