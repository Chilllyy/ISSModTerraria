using System;
using com.lightstreamer.client;
using log4net;
using Terraria.ModLoader;

namespace ISSDisplay
{
	public class ISSDisplay : Mod
	{
		internal static ISSDisplayContent display;
		internal static LightstreamerClient client;
		internal static ILog logger;
		public override void Load()
		{
			logger = Logger;
			client = new LightstreamerClient("https://push.lightstreamer.com", "ISSLIVE");
			client.connect();

			Subscription subscription = new Subscription("MERGE", ["NODE3000005"], ["Value"]);
			subscription.RequestedSnapshot = "yes";
			subscription.addListener(new SubListen());
			
			client.subscribe(subscription);
			
			
			
			display = new ISSDisplayContent();
			((Mod)this).AddContent((ILoadable)(object)display);
		}

		public override void Unload()
		{
			display = null;
		}

		internal class SubListen : SubscriptionListener
		{
			public void onClearSnapshot(string itemName, int itemPos)
			{
			}

			public void onCommandSecondLevelItemLostUpdates(int lostUpdates, string key)
			{
			}

			public void onCommandSecondLevelSubscriptionError(int code, string message, string key)
			{
			}

			public void onEndOfSnapshot(string itemName, int itemPos)
			{
			}

			public void onItemLostUpdates(string itemName, int itemPos, int lostUpdates)
			{
			}

			public void onItemUpdate(ItemUpdate itemUpdate)
			{
				string newValue = itemUpdate.getValue("Value");
				try
				{
					int val = int.Parse(newValue);
					display.value = val;
					logger.Debug($"Received new value from Lightstreamer {val}");
				}
				catch (ArgumentException e)
				{
					logger.Warn($"Value provided from Lightstreamer is null\n{newValue}\n{e}");
				}
				catch (FormatException e)
				{
					logger.Warn($"Value provided from Lightstreamer is not a number \n{newValue}\n{e}");
				}
			}

			public void onListenEnd()
			{
			}

			public void onListenStart()
			{
			}

			public void onSubscription()
			{
			}

			public void onSubscriptionError(int code, string message)
			{
			}

			public void onUnsubscription()
			{
			}

			public void onRealMaxFrequency(string frequency)
			{
			}
		}
	}
}
