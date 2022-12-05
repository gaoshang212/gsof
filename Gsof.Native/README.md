# Gsof.Native
* 支持动态加载 动态库
* 支持自动优先识别指定 `动态库` 目录面的 `x86` 或 `x64` 目录。
* 支持 `Linux` ，未测试。
* 支持 `Buffer` 类，简化内存操作。

## 使用

libtest.dll 为 中包括一个test函数

```csharp
int test(int input)
{
    return input;
}
```

### **方法名调用**

```csharp
int input = 0;
int result = -1;
using (var native = NativeFactory.Create(@"../../libtest.dll"))
{
    result = native.Invoke<int>("test", input);
}

```

### **dynamic 方式调用**

- 优点：调用方便，简单类型调用时，不用做过多的定义。
- 缺点：4.0下性能不理想，4.5+性能好很多，但相较于委托的方式，还差些。

```csharp
int input = 0;
int result = -1;
using (dynamic native = NativeFactory.Create(@"../../libtest.dll"))
{
    result = native.test<int>(input);
}

```

### **委托方式调用**

- 优化：效率高，没有了第一次动态构造委托的消耗，可获取到函数委托增加 重复调用消耗
- 缺点：如果函数较多，委托定义较为繁琐

```csharp
[NativeFuncton("test")]
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
delegate int Test(int p_sleep);

public void DelegateFunction()
{
    int input = 0;
    int result = -1;
    using (var native = NativeFactory.Create(@"../../libtest.dll"))
    {
        // 直接调用
        var result1 = native1.Invoke<int, Test>(input);

        // 获取函数委托调用
        var test = native.GetFunction<Test>();
        result = test(input);
    }

    Assert.AreEqual(input, result);
}
```

## Todo List

- [x] 支持自动识别 x86 和 x64 目录
- [ ] 支持 Linux 