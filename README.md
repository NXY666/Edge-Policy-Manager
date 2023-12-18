# Edge 策略管理器

![image](https://github.com/NXY666/EdgePolicyManager/assets/62371554/412f6aa7-22e4-4eae-8eb7-1440f8ce449d)

## 什么是“策略”？

Edge 策略通常用于企业或教育机构环境，使IT管理员能够集中管理和配置浏览器设置，以满足组织的特定需求和安全标准。

这些策略涵盖了从用户界面定制、扩展管理、网络配置到安全性和隐私设置的各个方面，执行特定的配置和限制。

## “策略”有什么用？

Edge 作为系统自带的浏览器，功能复杂且冗余。不仅如此，微软还经常通过弹窗和提示“推荐”那些我们不需要的功能。

通过策略，我们可以对 Edge 的功能进行定制，使其更符合我们的使用习惯。

例如：

* 禁用 `EdgeCollectionsEnabled` 策略可关闭 `集锦` 功能。
* 禁用 `ShowPDFDefaultRecommendationsEnabled` 策略可阻止弹出“将 Microsoft Edge 设置为默认 PDF 阅读器”的推荐提示。
* 禁用 `AllowSurfGame` 策略可阻止进入 `冲浪游戏` 。
* 启用 `DoubleClickCloseTabEnabled` 策略可启用双击关闭标签页功能（仅在中国可用，至少文档是这样写的）。

## 特性

* **策略浏览**。支持查看 `Edge` 、 `Edge Update` 、 `Edge Webview2` 所有可用策略的级别、支持版本、是否动态刷新、文档。
* **策略配置**。支持编辑类型为 `字符串（String）` 、 `布尔值（Boolean）` 、 `整形（Integer）` 的策略级别和值。
* **策略检索**。支持按策略名称、简介、文档内容检索策略。
* **多语言支持**。可用的显示语言有 `中文（简体）` 、 `中文（台湾）` 、 `英语（美国）` 。
* **安全锁**。使用意外的注册表路径时，将阻止对注册表的写入、删除操作。

## 说明

* 默认值根据文档内容推断，仅供参考。
* 暂不支持主动切换显示语言。