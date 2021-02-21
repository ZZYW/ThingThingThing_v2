using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoslynCSharp;
using System.IO;

namespace ThingSpace
{
    public class CreateDomain : MonoBehaviour
    {
        private ScriptDomain domain = null;
        public TextAsset testThingClass;
        public GameObject parent;
        public AssemblyReferenceAsset[] assemblyReferences;


        public string thingScriptDirectoryPath;

        public Mesh mesh;

        // Start is called before the first frame update
        void Start()
        {
            bool initCompiler = true;
            domain = ScriptDomain.CreateDomain("UsersThingDomain", initCompiler);

            foreach (AssemblyReferenceAsset reference in assemblyReferences)
                domain.RoslynCompilerService.ReferenceAssemblies.Add(reference);


            //read all Thing classes

            var info = new DirectoryInfo(thingScriptDirectoryPath);
            var fileInfo = info.GetFiles("*.cs");
            foreach (var file in fileInfo)
            {
                string filePath = Path.Combine(Application.dataPath, thingScriptDirectoryPath, file.ToString());
                Debug.Log(filePath);
                StreamReader reader = new StreamReader(filePath);
                CreateNewThing(reader.ReadToEnd());
                reader.Close();
            }
            
        }

        Thing CreateNewThing(string code)
        {
            var type = domain.CompileAndLoadMainSource(code);
            var go = new GameObject();
            ScriptProxy proxy = type.CreateInstance(go);
            var newThing = go.GetComponent<Thing>();
            if (ThingGod.ThingBornEvent != null) ThingGod.ThingBornEvent(newThing);
            return newThing;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}