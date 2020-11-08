
# DynamicCache
基于BTFindTree项目的动态缓存构造库


#### 使用方法(User Api)：  



### Natasha 初始化

  ```C#
  //仅仅注册组件
  NatashaInitializer.Initialize();
  //注册组件+预热组件 , 之后编译会更加快速
  await NatashaInitializer.InitializeAndPreheating();
  ```

<br/>  


```C#

dict.HashTree();
dict.PrecitionTree();

```
