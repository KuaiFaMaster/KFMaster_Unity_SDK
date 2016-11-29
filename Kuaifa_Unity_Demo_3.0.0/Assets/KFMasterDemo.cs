using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets
{
    /// <summary>
    /// 好接入SDKKIT Unity接口调用示例
    /// </summary>
    class KFMasterDemo : MonoBehaviour,IHJRSDKKitCallBack
    {
        private static AndroidJavaObject actvityJo;
        public static AndroidJavaObject AppContext
        {
            get
            {
                if (actvityJo == null)
                {
                    AndroidJavaClass activityJc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    actvityJo = activityJc.GetStatic<AndroidJavaObject>("currentActivity");
                }
                return actvityJo;
            }
        }


        void Awake() 
        { 
           

            Debug.Log("HJRSDKKitCore#Awake" );
        }


        // Use this for initialization
        void Start()
        {
            //初始化SDK
            HJRSDKKitUnityCore.Instance.Init(AppContext, gameObject.name); 
            Debug.Log("HJRSDKKitCore#Start");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
            {
                Application.Quit();
            }

        }


        void OnGUI()
        {

   
            //登录
            if (GUI.Button(new Rect(100, 50, 200, 50), "SDK Login"))
            {

                HJRSDKKitUnityCore.Instance.Login(AppContext);
            }


            //支付
            if (GUI.Button(new Rect(100, 150, 200, 50), "SDK Pay"))
            {

                int amount = 10;// 所购买商品金额, 单位为元 ,*必传
                int productId = 1001;// 购买商品的商品id，数字 *必传
                string productName = "药水";// 所购买商品名称，应用指定，*必传
                int productNum = 1;//购买数量 , 当商品ID 不为空时，*必传
                string orderId = "123";//订单号， *必传
                string productDesc = "我是商品描述";//商品描述， *必传
                string extInfo = "";//额外参数， 没有传""

                HJRSDKKitUnityCore.Instance.Pay(amount, productId, productName, productNum, orderId, productDesc, extInfo);
            }

            
            //
            //用户中心
            if (GUI.Button(new Rect(100, 250, 200, 50), "SDK UserCenter"))
            {
                HJRSDKKitUnityCore.Instance.UserCenter();
            }


           
            //注销
            if (GUI.Button(new Rect(100, 350, 200, 50), "SDK Logout"))
            {
                HJRSDKKitUnityCore.Instance.Logout();
            }
           
            //获取订单信息
            if (GUI.Button(new Rect(100, 450, 200, 50), "SDK OrderResult"))
            {
                string OrderId = "";
                HJRSDKKitUnityCore.Instance.OrderResult(OrderId);

            }
          

            //退出游戏
            if (GUI.Button(new Rect(100, 550, 200, 50), "SDK ExitGame"))
            {
                HJRSDKKitUnityCore.Instance.ExitGame(AppContext);
            }


            //-------------------统计部分接口-----------------------


            //创建角色，创建角色时调用
            if (GUI.Button(new Rect(100, 650, 200, 50), " Create role"))
            {

                string roleId = "1";//角色标识
                string roleName = "武士";
                string serverId = "101";///服务器编号
                string serverName = "华南一服";
                HJRSDKKitUnityCore.Instance.onCreateRole( roleId, roleName, serverId, serverName);
            }

            //支付，在支付成功之后调用，需要特别注意支付金额请以最终充值金额为准，务必一定传正确
            if (GUI.Button(new Rect(100, 750, 200, 50), "Pay order"))
            {

                int amount = 10;//充值金额
                string ordernum = "100001";//订单号
                string productdesc = "商品描述";//商品描述

                HJRSDKKitUnityCore.Instance.onPay(amount, ordernum, productdesc);

            }

            //在每次进入游戏时调用，必接
            if (GUI.Button(new Rect(100, 850, 200, 50), "Enter Game"))
            {
                string serverId = "101";//服务器编号
                string serverName = "华南一服";//服务器名称
                string roleId = "121";//角色唯一标识
                string roleName = "武士";//角色名称
                int roleLevel = 3;//玩家等级 
                HJRSDKKitUnityCore.Instance.onEnterGame(roleId, roleName, roleLevel, serverId, serverName,0L,0L);
            }
            //
            //玩家升级，在玩家升级时调用。需要实时调用
            if (GUI.Button(new Rect(100, 950, 200, 50), "Player upgrade"))
            {

                string level = "10";//玩家等级 ,不能传中文
                HJRSDKKitUnityCore.Instance.onUpgrade(level, 0L, 0L);
            }

            


        }
      
        /// 
        /// ************************Callback methods***********************************
        /// 

        /// 
        /// 接口回调数据均以Json形式返回
        /// status  ：状态，1：成功，-1：失败或者取消
        /// message : 本次请求的描述信息
        /// 支付回调接口：kitOrderId :订单号
       
        public void InitCallBack(string CallBackJson)
        {
            //{"message":"初始化成功","status":1}
            Debug.Log("HJRSDKKitCore#"+CallBackJson);
             
        }
        /// <summary>
        /// </summary>
        /// <param name="CallBackJson">
        /// 登录回调结构：loginUsserId:用户ID， loginAuthToken：用户token，loginUserNick:用户昵称，loginServerId：登录服务器ID ， cp:当前渠道的唯一标识号，switchUserFlag：true标识来自切换帐号登录回调false标识正常登录回调
        ///               其中：loginUserNick:，loginServerId可能会为空,
        /// </param>
        public void LoginCallBack(string CallBackJson)
        {
            //{"message":"登录成功","loginUserId":"8656@MINIGAME","loginOpenId":"1cb8ca6b6c44923b651039f42c2802cd","loginUserName":"","status":1,"loginAuthToken":"86a26fbf43167c3f33992c3141c682f0","cp":"kuaifa","switchUserFlag":"true"}
            Debug.Log("HJRSDKKitCore#" + CallBackJson);
        }

        public void LogoutCallBack(string CallBackJson)
        {
            //{"message":"注销成功","status":1}
            Debug.Log("HJRSDKKitCore#" + CallBackJson);
        }

        public void PayCallBack(string CallBackJson)
        {
            //{"message":"登录成功","kitOrderId":"1000156235","status":1}
            Debug.Log("HJRSDKKitCore#" + CallBackJson);
        }

        public void ExitGameCallBack(string CallBackJson)
        {
            //{"message":"确定","status":1}
            //{"message":"取消","status":-1}
            Debug.Log("HJRSDKKitCore#" + CallBackJson);
        }

        public void OrderResultCallBack(string CallBackJson)
        {
            //{"message":"获取订单结果信息成功","status":1}
            Debug.Log("HJRSDKKitCore#" + CallBackJson);
        }
    }
}
