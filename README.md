方案名稱:BurgerBaby速食店網路平台 (後台、API 與前台)

方案描述:

此方案包含三個子系統專案，實現從後台管理到前台展示的簡易網站解決方案。



1.後台管理系統專案 (Core MVC)

提供會員與商品的基礎管理功能，包括圖片上傳與預覽。

支援多頁籤操作與搜尋功能，方便管理操作。

採用三層式架構，請求回應收發、邏輯、資料存取分層，並透過 DI 容器達成 IoC（控制反轉）。



2.API 前台後端系統專案 (Web API)

作為前台的數據支持層，提供商品與會員相關的 RESTful API。

採用 Cookie 驗證登入，返回 HttpOnly 的 Refresh Token，並生成 JWT Access Token。

使用短時效 Access Token 降低伺服器負擔，並且將Jwt存在VUEX參數減少整體專案受到CSRF及XSS攻擊的可能性。



3.前台前端系統專案 (Vue)

基於 Vue CLI 實現的單頁應用程式 (SPA)。

採用 Vuex 管理全域狀態，並使用 Vue Router 實現路由管理。

支援商品圖片預覽與放大鏡功能，提升用戶體驗。



資料模型設計

關聯結構:

Roles 與 Members：一對多關聯，用於角色管理。

Products 與 Imgs：一對多關聯，用於商品與圖片管理。



系統功能

1.後台管理系統 (Core MVC)

會員管理：

管理權限與會員關係。



2.商品管理：

管理商品及其圖片。

支援圖片上傳、預覽與放大鏡功能。



3.登入與驗證：

使用 Cookie 驗證登入，確保後台安全性。



4.頁籤功能：

支援多頁籤操作，提升管理效率。

5.搜尋功能:
支援會員、商品、圖片、規則等搜尋篩選功能。



API 系統 (Web API)



系統功能:提供前台前端Vue專案需要之API，並可使用Swagger平台進行功能測試。

認證機制：

採用 Cookie 驗證登入，返回長時效性 HttpOnly Refresh Token。

利用 Refresh Token 生成短時效 JWT Access Token。

數據接口：

提供會員與商品的相關數據 API。

API 請求需通過 JWT 驗證，確保安全性。



前台系統 (Vue)

單頁應用程式 (SPA)：

基於 Vue CLI 開發，支援商品瀏覽與會員功能。

狀態管理：

使用 Vuex 管理用戶狀態與登入資訊。

路由管理：

使用 Vue Router 處理前台路由。

會員功能；
1.提供註冊為一般會員。
2.提供一般會員登入。
3.提供一般會員修改用戶資料。



商品圖片與展示功能：

1.提供商品相關訊息。
2.支援商品圖片預覽與放大鏡效果。

搜尋功能:
依據關鍵字查找相關商品訊息。



BurgerBaby 專案部署簡易技術文件

1請先安裝以下套件：



.NET 6 Hosting Bundle

https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.36-windows-hosting-bundle-installer

IIS URL Rewrite Module

下載網址：

https://www.iis.net/downloads/microsoft/url-rewrite



2安裝完成後請在 CMD 輸入：iisreset



3部署專案

先將IIS.zip解壓縮後

IIS資料夾內包含已打包完成的三個專案：



BurgerBaby → http://localhost:7228/



BurgerBabyApi → https://localhost:7266/



BurgerBabyFE → http://localhost:8082/



4SQL 初始化



需先執行專案提供的 SQL 指令建立資料庫與相關資料表。



5建立資料庫登入帳號



需手動建立可讀寫資料庫的 SQL Login：



帳號：bb10



密碼：bb10

