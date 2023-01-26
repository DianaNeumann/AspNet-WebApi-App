# Message Processing System


## Контроллеры

---

#### Account-Controller
Сделал в большей части для проверки работоспособности авторизации

![image](https://user-images.githubusercontent.com/56086653/210601587-2cefc56a-586f-4e46-a13d-ff3b048ea4ea.png)

--- 

#### Auth-Controller
Занимается добавлением новых пользователей в систему и выдачу им JWT токенов

![image](https://user-images.githubusercontent.com/56086653/210601885-c3e2d343-0853-4bbc-8274-37a4160278aa.png)

Метод Login выплевывает JWT токен, с помощью которого мы можем авторизоваться в сваггере:

![image](https://user-images.githubusercontent.com/56086653/210603310-9fce56ef-f1ae-46aa-bfc6-629c9ac68e0d.png)

![image](https://user-images.githubusercontent.com/56086653/210603685-a31572f4-be49-43b2-b1fe-91cd4b0f1fcb.png)

![image](https://user-images.githubusercontent.com/56086653/210603844-c2f217d8-23b2-48a4-aebf-c74c6eea7b32.png)

---

#### Message-Controller 

Обработка/добавление сообщений в систему, в зависимости от иерархии сотрудника.
'CreateMessage' доступен любому пользователю, поэтому не требует каких-то прав.

![image](https://user-images.githubusercontent.com/56086653/210602409-d87f071f-1216-43a5-8221-f94ec6808124.png)

---

#### Report-Controller
Создает отчет. При создании отчета на яндекс диск ***Компании*** заливается PDF файл   

![image](https://user-images.githubusercontent.com/56086653/210602983-50fba8da-4c4b-4ca7-8164-88d5bbc5b747.png)

![image](https://user-images.githubusercontent.com/56086653/210604113-5ad106bf-2807-4182-bca2-7a26f7b0b2c0.png)

![image](https://user-images.githubusercontent.com/56086653/210604290-68a07c8e-8fbc-44b2-b349-d640f6e25f8d.png)

![image](https://user-images.githubusercontent.com/56086653/210604427-4c673f4e-7536-45ab-9d06-2ae23644f9b3.png)

пдфка))
У сотрудников более высокго уровня вместо сообщний будут ссылки на яндекс диск отчетов нижестоящий сотрудников (с их именами и временем создания отчета)

![image](https://user-images.githubusercontent.com/56086653/210605126-a836cbe4-1646-4d73-a76e-eae4cc057e4f.png)

* токен для доступа в диску хранится в конфигурационном файле доменного слоя 

![image](https://user-images.githubusercontent.com/56086653/210604772-bf6222e5-215c-4b76-aa77-631a3b756d3b.png)


