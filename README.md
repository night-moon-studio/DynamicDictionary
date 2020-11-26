
# DynamicCache
基于BTFindTree项目的动态缓存构造库


#### 使用方法(User Api)：  

 <br/>  
 
 - 引入 动态构件库： NMS.DynamicDictionary

 - 初始化 Natasha ： NatashaInitializer.InitializeAndPreheating();

```C#

var fastDict = dict.HashTree / PrecitionTree / FuzzyTree();
var result = fastDict["a"];
fastDict["a"] = "b";
fastDict["other"] = "b"; //ERROR dict 在生成时未带有 other 键


```
