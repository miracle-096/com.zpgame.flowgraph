﻿using UnityEditor.Experimental.GraphView;

namespace FlowGraph.Node
{
    public class ActionNodeView : BaseNodeView<BaseAction>
    {
        public ActionNodeView()
        {
            //Action有一个输出端口一个输入端口,输入接口可以多连接
            Port input = GetPortForNode(this, Direction.Input, Port.Capacity.Multi);
            Port output = GetPortForNode(this, Direction.Output, Port.Capacity.Single);
            input.portName = "input";
            output.portName = "output";

            title = state != null ? state.name : "ActionNode";

            inputContainer.Add(input);
            outputContainer.Add(output);
        }

        public override void OnEdgeCreate(Edge edge)
        {
            base.OnEdgeCreate(edge);

            BaseNodeView targetView = edge.input.node as BaseNodeView;
            state.nextFlow = targetView.state as MonoState;
        }

        public override void OnEdgeRemove(Edge edge)
        {
            base.OnEdgeRemove(edge);

            state.nextFlow = null;
        }
    }
}
