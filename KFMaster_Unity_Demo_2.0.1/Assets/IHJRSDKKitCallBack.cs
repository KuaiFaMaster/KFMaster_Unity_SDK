using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets
{
    /// <summary>
    /// 好接入回调接口
    /// </summary>
    interface IHJRSDKKitCallBack
    {
        /// <summary>
        /// 初始化回调
        /// </summary>
        /// <param name="CallBackJson"></param>
        void InitCallBack(string CallBackJson);

        /// <summary>
        /// 登录回调
        /// </summary>
        /// <param name="CallBackJson"></param>
        void LoginCallBack(string CallBackJson);

        /// <summary>
        /// 注销回调
        /// </summary>
        /// <param name="CallBackJson"></param>
        void LogoutCallBack(string CallBackJson);

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="CallBackJson"></param>
        void PayCallBack(string CallBackJson);

        /// <summary>
        /// 退出游戏回调
        /// </summary>
        /// <param name="CallBackJson"></param>
        void ExitGameCallBack(string CallBackJson);

        /// <summary>
        /// 获取订单结果回调
        /// </summary>
        /// <param name="CallBackJson"></param>
        void OrderResultCallBack(string CallBackJson);

    }
}
