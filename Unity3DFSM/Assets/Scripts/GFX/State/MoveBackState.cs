﻿using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


//  MoveBackState.cs
//  Author: Lu Zexi
//  2013-11-29



namespace Game.Gfx
{

    /// <summary>
    /// 回退状态
    /// </summary>
    public class MoveBackState : StateBase
    {
        private Vector3 m_vecTargetPos;     //目标点
        private float m_fCostTime;          //花费时间
        private float m_fLastTime;          //最近时间
        private Vector3 m_vecLastPos;       //最近坐标

        public MoveBackState(GfxObject obj)
            : base(obj)
        { 
        }

        /// <summary>
        /// 获取状态类型
        /// </summary>
        /// <returns></returns>
        public override STATE_TYPE GetStateType()
        {
            return STATE_TYPE.STATE_MOVE_BACK;
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="costTime"></param>
        public void Set(Vector3 pos, float costTime)
        {
            this.m_vecTargetPos = pos;
            this.m_fCostTime = costTime;
            this.m_fLastTime = Time.fixedTime;
            this.m_vecLastPos = this.m_cObj.transform.localPosition;
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        /// <returns></returns>
        public override bool OnEnter()
        {
            this.m_cObj.Play("idle", WrapMode.Once, 1f, PLAY_MODE.PLAY);
            return true;
        }

        /// <summary>
        /// 逻辑更新
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            float disTime = Time.fixedTime - this.m_fLastTime;

            if (disTime >= this.m_fCostTime)
            {
                this.m_cObj.transform.localPosition = this.m_vecTargetPos;
                return false;
            }
            Vector3 pos = Vector3.Lerp(this.m_vecLastPos, this.m_vecTargetPos, disTime / this.m_fCostTime);
            this.m_cObj.transform.localPosition = pos;

            return true;
        }
    }

}