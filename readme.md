# C#使用HttpWebRequest用Post提交MultiPart数据

## 1 先上真实场景分析



### 1.1 标头

#### 常规

```txt
请求 URL: http://ip/marry/.../sadf.do?method=uploadFiles
请求方法: POST
状态代码: 200 OK
远程地址: ip:80
引用站点策略: strict-origin-when-cross-origin
```

#### 响应头

```http
HTTP/1.1 200 OK
Server: nginx
Date: Sun, 30 May 2021 09:12:44 GMT
Content-Type: application/json;charset=UTF-8
Content-Length: 105
Connection: keep-alive
Cache-Control: no-store
Expires: Thu, 01 Jan 1970 00:00:00 GMT
Pragma: no-cache
P3P: CP="CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR"
backendIP: backendIP:8006
backendCode: 200
```

分析后:

```http
backendCode: 200
backendIP: backendIP:8006
Cache-Control: no-store
Connection: keep-alive
Content-Length: 105
Content-Type: application/json;charset=UTF-8
Date: Sun, 30 May 2021 09:12:44 GMT
Expires: Thu, 01 Jan 1970 00:00:00 GMT
P3P: CP="CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR"
Pragma: no-cache
Server: nginx
```

#### 请求标头

```http
POST /marry/.../sadf.do?method=uploadFiles HTTP/1.1
Host: ip
Connection: keep-alive
Content-Length: 2319410
HHCSRFToken: dac4324e1-b436-436e-af234a-61casfasd4b3
Accept: */*
X-Requested-With: XMLHttpRequest
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36 Edg/91.0.864.37
Content-Type: multipart/form-data; boundary=----WebKitFormBoundaryH9ldKy0i85DnB8Ey
Origin: http://ip
Accept-Encoding: gzip, deflate
Accept-Language: zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6
Cookie: JSESSIONID=asfasdf3efdsfqafewafd
```



#### 查询字符串参数

```http
method=uploadFiles
```



#### 表单数据

```http
------WebKitFormBoundaryqdR6BBr1A4ByeGuX
Content-Disposition: form-data; name="file"; filename="J370211-2016-003315.pdf"
Content-Type: application/pdf

------WebKitFormBoundaryqdR6BBr1A4ByeGuX--
```



### 1.2 响应

```json
{"successList":[{"fileName":"J370211-2016-003315.pdf","success":"成功","failReason":""}],"failList":[]}
```

上传两个时:

```json
{"successList":[{"fileName":"J370211-2016-003315.pdf","success":"成功","failReason":""},{"fileName":"J370211-2016-003316.pdf","success":"成功","failReason":""}],"failList":[]}
```



### 1.4 Cookie

| 名称       | 值                    | Domain | Path    | Expires/Max-Age | 大小 | HttpOnly | Security | SameSite | sameParty | Priority |
| ---------- | --------------------- | ------ | ------- | --------------- | ---- | -------- | -------- | -------- | --------- | -------- |
| JSESSIONID | asfasdf3efdsfqafewafd | ip     | /marry/ | 会话            | 42   | ✓        |          |          |           | Medium   |



## 2 根据[RFC 2045](http://www.w3.org/TR/html401/interact/forms.html#h-17.13.4)协议分析

一个Http Post的数据格式如下, 在上面示例的基础上略作扩展：

```http
Content-Type: multipart/form-data; boundary=----WebKitFormBoundaryH9ldKy0i85DnB8Ey

 ------WebKitFormBoundaryH9ldKy0i85DnB8Ey
 Content-Disposition: form-data; name="submit-name"
 Larry
  
 ------WebKitFormBoundaryH9ldKy0i85DnB8Ey
Content-Disposition: form-data; name="file"; filename="J370211-2016-003315.pdf"
Content-Type: application/pdf

 ... contents of J370211-2016-003315.pdf ...

------WebKitFormBoundaryH9ldKy0i85DnB8Ey--
```



解析:

> ```http
> Content-Type: multipart/form-data; boundary=----WebKitFormBoundaryH9ldKy0i85DnB8Ey
> ```
>
> 首先声明数据类型为multipart/form-data, 然后定义边界字符串----WebKitFormBoundaryH9ldKy0i85DnB8Ey，这个边界字符串就是用来在下面来区分各个数据的，可以随便定义，但是最好是用破折号等数据中一般不会出现的字符。然后是换行符。
>
> 注意：Post中定义的换行符是\r\n



> ```http
>  ------WebKitFormBoundaryH9ldKy0i85DnB8Ey
> ```
>
> 这个是边界字符串，注意每一个边界符前面都需要加2个连字符“--”，然后跟上换行符。



> ```http
>  Content-Disposition: form-data; name="submit-name"
>  Larry
> ```
>
> 这里是Key-Value数据中字符串类型的数据。 submit-name 是这个Key-Value数据中的Key。当然也需要换行符。
>
> `Larry`就是Key-Value数据中的value。
>
> ```http
> ------WebKitFormBoundaryH9ldKy0i85DnB8Ey
> ```
>
> 边界符,表示数据结束。



> ```http
> Content-Disposition: form-data; name="file"; filename="J370211-2016-003315.pdf"
> ```
>
> 这个代表另外一个数据，它的key是file，文件名是J370211-2016-003315.pdf。 注意：最后面没有分号了。
>
> ```http
> Content-Type: application/pdf
> ```
>
> 这个标识文件类型. 另外`application/octet-stream`表示二进制数据。



> ```http
>  ... contents of J370211-2016-003315.pdf ...
> ```
>
> 这个是文件内容。可以使二进制的数据。
>
> ```http
> ------WebKitFormBoundaryH9ldKy0i85DnB8Ey--
> ```
>
> 数据结束后的分界符，注意因为这个后面没有数据了所以需要在后面追加一个“--”表示结束。

