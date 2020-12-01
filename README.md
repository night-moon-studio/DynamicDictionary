
# DynamicDictionary
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

#### 性能测试

![benchmark](https://images.gitee.com/uploads/images/2020/1201/172724_a9004a04_1478282.png)

<br/>

#### 捐赠  

<img width=200 height=200 src="https://images.gitee.com/uploads/images/2020/1201/163955_a29c0b44_1478282.png" title="Scan and donate"/><img width=200 height=200 src="https://images.gitee.com/uploads/images/2020/1201/164809_5a67d5e2_1478282.png" title="Scan and donate"/>

      
      
