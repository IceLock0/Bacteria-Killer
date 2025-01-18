using System.Collections.Generic;
using UnityEngine;
using View.Characters.Player.Upgrade;

namespace Utils.Factory.UI
{
    public interface IUpgradeViewFactory
    {
        public List<PlayerUpgradeView> CreateUpgradeViews(Transform root);
    }
}