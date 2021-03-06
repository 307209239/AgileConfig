﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Agile.Config.Client
{
    public class AgileConfigProvider : ConfigurationProvider
    {
        private ConfigClient Client { get; }

        public AgileConfigProvider(IConfigClient client)
        {
            Client = client as ConfigClient;
        }

        /// <summary>
        /// load方法会通过http从服务端拉取所有的配置，需要注意的是load方法在加载所有配置后会启动一个websocket客户端跟服务端保持长连接，当websocket
        /// 连接建立成功会调用一次load方法，所以系统刚启动的时候通常会出现两次http请求。
        /// </summary>
        public override void Load()
        {
            Client.ConnectAsync().GetAwaiter().GetResult() ;
            Data = Client.Data;
        }
    }
}
