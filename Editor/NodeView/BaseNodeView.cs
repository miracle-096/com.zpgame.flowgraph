﻿using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace FlowGraph.Node
{
    public abstract class BaseNodeView : UnityEditor.Experimental.GraphView.Node
    {
        /// <summary>
        /// 点击该节点时被调用的事件，比如转发该节点信息到Inspector中显示
        /// </summary>
        public Action<BaseNodeView> OnNodeSelected;

        public TextField textField;
        public string GUID;

        public BaseNodeView() : base()
        {
            textField = new TextField();
            GUID = Guid.NewGuid().ToString();
            
        }
        // 为节点n创建input port或者output port
        // Direction: 是一个简单的枚举，分为Input和Output两种
        public Port GetPortForNode(BaseNodeView n, Direction portDir, Port.Capacity capacity = Port.Capacity.Single)
        {
            // Orientation也是个简单的枚举，分为Horizontal和Vertical两种，port的数据类型是bool
            return n.InstantiatePort(Orientation.Horizontal, portDir, capacity, typeof(bool));
        }

        //告诉Inspector去绘制该节点
        public override void OnSelected()
        {
            base.OnSelected();
            Debug.Log($"{this.name}节点被点击");
            OnNodeSelected?.Invoke(this);
        }

        public override void OnUnselected()
        {
            base.OnUnselected();
        }
        public abstract NodeState state { get; set; }

        public virtual void OnEdgeRemove(Edge edge)
        {

        }
        public virtual void OnEdgeCreate(Edge edge)
        {

        }
    }


    public class BaseNodeView<State> : BaseNodeView where State : NodeState
    {
        /// <summary>
        /// 关联的State
        /// </summary>
        private State _state;

        public override NodeState state 
        { 
            get
            {
                return _state;
            }
            set
            {
                if (_state != null)
                    _state.node = null;

                _state = (State)value;
                _state.node = this;

                if (_state != null)
                {
                    title = _state.GetNodeName();
                }
            }
        }
    }
}