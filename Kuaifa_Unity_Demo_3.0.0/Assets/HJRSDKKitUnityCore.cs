using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Assets
{
    class HJRSDKKitUnityCore
    {
        private const string SDK_JAVA_CLASS = "com.hjr.sdkkit.unity.core.HJRSDKKitUnityCore";

        /// <summary>
        /// 成功状态
        /// </summary>
        public  static int STATUS_SUCCESS = 1;

        /// <summary>
        /// 失败状态
        /// </summary>
        public  static int STATUS_FAILE = -1;

        private  IHJRSDKKitCallBack sCallBack2Game;

        private static HJRSDKKitUnityCore _instance;
        public static  HJRSDKKitUnityCore Instance
        {
           get
           {
                if (null == _instance)
                {
                    _instance = new HJRSDKKitUnityCore();
                }
                return _instance;
           }
           
            
         
        }
       


        /// <summary>
        /// SDK初始化
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="gameObj"></param>
        public void Init(AndroidJavaObject Context,string gameObj)
        {
          
            CallSdkApi("init", Context, gameObj);
        }
        /// <summary>
        /// 登录
        /// </summary>
        public void Login(AndroidJavaObject Context)
        {
            CallSdkApi("login", Context);
        }

       

        /// <summary>
        /// 用户中心
        /// </summary>
        public void UserCenter()
        {
            CallSdkApi("userCenter", null);
        }

       

        /// <summary>
        /// 注销
        /// </summary>
        public void Logout()
        {
            CallSdkApi("logout", null);
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="productId"></param>
        /// <param name="productName"></param>
        /// <param name="productNum"></param>
        /// <param name="productDesc"></param>
        /// <param name="extInfo"></param>
        public void Pay(int amount ,int productId,string productName, int productNum,string orderId,string productDesc ,string extInfo)
        {

            CallSdkApi("pay", amount, productId, productName, productNum, orderId,  productDesc, extInfo);
        }

       /// <summary>
       /// 获取订单结果
       /// </summary>
       /// <param name="orderId"></param>
        public void OrderResult(string orderId)
        {
            CallSdkApi("getOrderInfo", orderId);
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        public void ExitGame(AndroidJavaObject Context)
        {
            CallSdkApi("exitGame", Context);
        }

      


        ///
        ///-----------------统计相关接口-----------------
        ///
        


        /// <summary>
        /// 统计支付
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="orderNumber"></param>
        /// <param name="productDesc"></param>
        public void onPay(int amount, string orderNumber,  string productDesc)
        {
            CallSdkApi("onPay", amount ,orderNumber,productDesc);
            
        }

        /// <summary>
        /// 统计角色升级
        /// </summary>
        /// <param name="level"></param>
        ///         /// <param name="createTime">
        ///角色创建时间
        ///1.需要发uc九游渠道则必传，long类型 时间戳，十位数，特别注意不能取系统当前时间，需要传服务器的角色创建时间，服务端需要保存该值
        ///详情可查看uc官方要求说明：http://bbs.9game.cn/thread-5370208-1-1.html
        ///2.不需要上uc九游渠道，则传0L
        /// </param>
        /// <param name="upgradeTime">
        /// 角色升级时间时间
        ///需要发uc九游渠道则必传，要求与以上
        ///不需要上uc九游渠道，则传0L
        /// </param>
        public void onUpgrade(string level, long createTime, long upgradeTime)
        {
            CallSdkApi("onUpgrade", level, createTime, upgradeTime);
        }

        /// <summary>
        /// 统计创建角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="serverId"></param>
        /// <param name="serverName"></param>

        public void onCreateRole(string roleId,string roleName, string serverId, string serverName)
        {
            CallSdkApi("onCreateRole", roleId, serverId, serverName, roleName);
        
        }



        /// <summary>
        /// 统计进入游戏
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <param name="roleLevel"></param>
        /// <param name="serverId"></param>
        /// <param name="serverName"></param>
        ///         /// <param name="createTime">
        ///角色创建时间
        ///1.需要发uc九游渠道则必传，long类型 时间戳，十位数，特别注意不能取系统当前时间，需要传服务器的角色创建时间，服务端需要保存该值
        ///详情可查看uc官方要求说明：http://bbs.9game.cn/thread-5370208-1-1.html
        ///2.不需要上uc九游渠道，则传0L
        /// </param>
        /// <param name="upgradeTime">
        /// 角色升级时间时间
        ///需要发uc九游渠道则必传，要求与以上
        ///不需要上uc九游渠道，则传0L
        /// </param>
        public void onEnterGame(string roleId, string roleName, int roleLevel, string serverId, string serverName,long createTime,  long upgradeTime)
        {
            CallSdkApi("onEnterGame", roleId, roleLevel, roleName,  serverId, serverName, createTime,   upgradeTime);
        
        }

        /// <summary>
        /// 调用SDK 接口
        /// </summary>
        /// <param name="apiName"></param>
        /// <param name="args"></param>
        private static void CallSdkApi(string apiName, params object[] args)
        {
           
            using (AndroidJavaClass cls = new AndroidJavaClass(SDK_JAVA_CLASS))
            {
                Debug.Log("Begin to request sdk api : " + apiName);
                if (null == args)
                { 
                    args = new object[]{};
                }
                cls.CallStatic(apiName, args);
            }
        }
       
     
    }
}
