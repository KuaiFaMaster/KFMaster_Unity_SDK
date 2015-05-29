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
                string serverId = "11";// 区服ID ， *必传
                string serverName = "第一服";//区服名， *必传
                string roleId = "1003";//角色ID， *必传
                string roleName = "精灵";//角色名， *必传
                string roleLevel = "";//角色等级,没有传""
                string userId = "627";//用户ID， *必传
                string userName = "张三";//用户名， *必传
                string extInfo = "";//额外参数， 没有传""

                HJRSDKKitUnityCore.Instance.Pay(amount, productId, productName, productNum, orderId, serverId, serverName, roleId, roleName, roleLevel, userId, userName, extInfo);
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


            //登录，登录成功并且选择完区服之后调用
            if (GUI.Button(new Rect(100, 650, 200, 50), " Login"))
            {


                string usermark = "123";//  用户ID
                string serverId = "101";///服务器编号
                HJRSDKKitUnityCore.Instance.OnLogin(usermark, serverId);
            }

            //创建角色，创建角色时调用
            if (GUI.Button(new Rect(100, 750, 200, 50), " Create role"))
            {

                string usermark = "101";// 用户标ID
                string roleId = "1";//角色标识
                string roleName = "武士";
                string serverId = "101";///服务器编号
                string serverName = "华南一服";
                HJRSDKKitUnityCore.Instance.onCreateRole(usermark, roleId, roleName, serverId, serverName);
            }

            //支付，在支付成功之后调用，需要特别注意支付金额请以最终充值金额为准，务必一定传正确
            if (GUI.Button(new Rect(100, 850, 200, 50), "Pay order"))
            {

                int amount = 10;//充值金额
                string serverId = "101";//服务器ID
                string serverName = "华南一服";//服务器名称
                string userId = "1001";//用户标识
                string roleId = "121";//角色唯一标识
                string roleName = "武士";//角色名称
                string ordernum = "100001";//订单号
                string userlevel = "3";//玩家等级
                string productdesc = "商品描述";//商品描述

                HJRSDKKitUnityCore.Instance.onPay(amount, serverId, serverName, userId, roleId, ordernum, userlevel, productdesc, roleName);

            }

            //游戏按钮点击，在点击某按钮时调用，非必接
            if (GUI.Button(new Rect(100, 950, 200, 50), "Game button clicked"))
            {
                string name = "升级武器装备";
                string usermark = "101";
                HJRSDKKitUnityCore.Instance.onButtonClick(name, usermark);
            }
            //
            //玩家升级，在玩家升级时调用。需要实时调用
            if (GUI.Button(new Rect(100, 1050, 200, 50), "Player upgrade"))
            {
                string userId = "101";//用户标识
                string serverId = "101";//服务器编号
                string serverName = "华南一服";//服务器名称
                string roleId = "1008611";//角色唯一标识
                string roleName = "武士";//角色名称
                string level = "10";//玩家等级 ,不能传中文
                HJRSDKKitUnityCore.Instance.onUpgrade(userId, serverId, serverName, level, roleId, roleName);
            }

            //
            //上传区服角色信息，在登录成功成功拿到角色以及区服信息是调用
            if (GUI.Button(new Rect(100, 1150, 200, 50), "Submit RoleServer inforamtion"))
            {
                string serverId = "101";//服务器编号
                string serverName = "华南一服";//服务器名称
                string roleId = "121";//角色唯一标识
                string roleName = "武士";//角色名称
                int roleLevel = 3;//玩家等级 
                string partyName = "角色所在帮派或者工会名称";//角色所在帮派或者工会名称
                string vipLevel = "VIP等级";//VIP等级
                HJRSDKKitUnityCore.Instance.onServerRoleInfo(roleId, roleName, roleLevel, serverId, serverName, partyName, vipLevel);
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
        /// 登录回调结构：loginUsserId:用户ID， loginAuthToken：用户token，loginUserNick:用户昵称，loginServerId：登录服务器ID 
        ///               其中：loginUserNick:，loginServerId可能会为空
        /// 
        public void InitCallBack(string CallBackJson)
        {
            //{"message":"初始化成功","status":1}
            Debug.Log("HJRSDKKitCore#"+CallBackJson);
             
        }

        public void LoginCallBack(string CallBackJson)
        {
            //{"message":"登录成功","loginUserId":"8656@MINIGAME","loginOpenId":"1cb8ca6b6c44923b651039f42c2802cd","loginUserName":"","status":1,"loginAuthToken":"86a26fbf43167c3f33992c3141c682f0"}
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
