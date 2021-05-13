### 需求分析
点击宝箱展示弹窗，弹窗包括宝箱和特效，点击宝箱展示宝箱打开动画和飞出金币。
使用 image 展示宝箱。
使用 button 实现购买。
点击宝箱后的 panel 弹窗应包括宝箱和特效。
点击 panel 中的宝箱飞出金币，使用animation控制金币动画，使用dotween控制金币飞出。

### Script
首先应导入相应插件和资源。
将dotween和simple文件导入放入plugins。
应包含三个基本目录：
base，function，utils

##### 基本目录
BuyTreasureChest：包括data部分和ui。
###### data

###### UI
HomePage:打开商店。
StoreDialog：加载商店页面。
CheseItem：加载宝箱item，和购买操作等。

###### base
ObjectPool:对象池，创建金币队列。
