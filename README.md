
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

#### License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fdotnetcore%2FNatasha.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Fdotnetcore%2FNatasha?ref=badge_large)          
      
