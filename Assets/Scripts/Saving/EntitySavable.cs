using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BOARDSGATE.Saving{
    [ExecuteAlways]
    public class EntitySavable : MonoBehaviour
    {
        [SerializeField] string uniqueID="";

#if UNITY_EDITOR//打包项目时不包括此段
        private void Update() {
            //需要在编辑界面就给场景中的实例生成GUID但同时不能给prefab生成
            if(Application.IsPlaying(this)) return;
            if(string.IsNullOrEmpty(gameObject.scene.path)) return;
            SerializedObject serializedObject=new SerializedObject(this);
            SerializedProperty property=serializedObject.FindProperty("uniqueID");
            if(string.IsNullOrEmpty(property.stringValue)){
                property.stringValue=System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif

        public string GetUniqueID(){
            return "";
        }

        public object GetStates(){
            return null;
        
        }

        public void RestoreStates(object states){

        }
    }
}

