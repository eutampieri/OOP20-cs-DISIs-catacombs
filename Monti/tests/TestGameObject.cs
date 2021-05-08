using Monti.src;
using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestGameObject
{
    [TestMethod]
    public void TestCreation()
    {
        ArrayList Objects = new ArrayList();
        for (int i = 0; i < 10; i++)
        {
            GameObject Enemy = new Object(0, i, GameObject.GameObjectType.ENEMY, GameObject.Team.ENEMY);
            Objects.Add(Enemy);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject Item = new Object(1, i, GameObject.GameObjectType.PICKUP, GameObject.Team.FREIND);
            Objects.Add(Item);
        }

        Objects.Add(new Object(1, i, GameObject.GameObjectType.PLAYER, GameObject.Team.FREIND));

        foreach (GameObject obj in Objects)
        {
            Assert.IsTrue(obj != null);
        }
    }

    [TestMethod]
    public void TestPosition()
    {
        ArrayList Objects = new ArrayList();
        for (int i = 0; i < 20; i++)
        {
            GameObject Item = new Object(1, i, GameObject.GameObjectType.PICKUP, GameObject.Team.FREIND);
            Objects.Add(Item);
        }
        foreach (GameObject obj in Objects)
        {
            Assert.IsTrue(obj.GetPosX() == 1 && obj.GetPosY() >= 0);
        }
    }

    [TestMethod]
    public void TestSpeed()
    {
        ArrayList Objects = new ArrayList();
        for (int i = 0; i < 20; i++)
        {
            GameObject Item = new Object(1, i, GameObject.GameObjectType.PICKUP, GameObject.Team.FREIND);
            Item.SetSpeed(i+1, i+1);
            Objects.Add(Item);
        }
        foreach (GameObject obj in Objects)
        {
            Assert.IsTrue(obj.GetSpeedX() > 0 && obj.GetSpeedY() > 0);
        }
    }

    [TestMethod]
    public void TestKindAndTeam()
    {
        GameObject Item = new Object(1, i, GameObject.GameObjectType.PICKUP, GameObject.Team.FREIND);
        GameObject Enemy = new Object(1, i, GameObject.GameObjectType.ENEMY, GameObject.Team.ENEMY);
        GameObject Player = new Object(1, i, GameObject.GameObjectType.PLAYER, GameObject.Team.FREIND);

        Assert.IsTrue(Item.GetKind() == GameObject.GameObjectType.PICKUP && Item.GetTeam() == GameObject.Team.FREIND);
        Assert.IsTrue(Enemy.GetKind() == GameObject.GameObjectType.ENEMY && Enemy.GetTeam() == GameObject.Team.ENEMY);
        Assert.IsTrue(Player.GetKind() == GameObject.GameObjectType.PLAYER && Player.GetTeam() == GameObject.Team.FREIND);
    }

}