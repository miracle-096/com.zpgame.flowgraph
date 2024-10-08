using UnityEngine;

namespace FlowGraph.Node
{
    public class CreateGameObjectAction : BaseAction
    {
        [Header("CreateGameObjectAction")]
        public GameObject protype;
        public Transform startPos;
        public Transform parent;

        public override void RunningLogic(BaseTrigger emitTrigger)
        {
            GameObject.Instantiate(protype, startPos.transform.position, Quaternion.identity, parent);

            RunOver(emitTrigger);
        }
    }

}