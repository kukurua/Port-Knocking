# Port Knocking

## Описание
Это программа поможет вам быстро осуществлять открытие портов с помощью Port Knocking.

## Техническая информация
Программа будет использовать протокол TCP для простукивания портов. Убедитесь, что ваши правила фаервола настроены на этот протокол. 
Для того чтобы прграмма понимала, что ей удалось достучаться до необходимого порта - неопходимо настроить ответ tcp-reset.

Разместите в папке с программой фалй port.conf в котором будут вписаны построчно порты в верном порядке.

Возможно вам понадобится установить пакет [.Net 8.0](https://dotnet.microsoft.com/ru-ru/download/dotnet/8.0)

# Пример файла port.conf
```
1111
1112
1113
1114
```

# Пример настройки цепочки в Router OS

```
 0    ;;; security: Port Knoking
      chain=Security Port Knock action=add-src-to-address-list protocol=tcp src-address-list=knock 2 address-list=knock 3 address-list-timeout=2h dst-port=1113 
      log=yes log-prefix="accept port knocking" 

 1    chain=Security Port Knock action=reject reject-with=tcp-reset protocol=tcp src-address-list=knock 2 dst-port=1113 log=no log-prefix="" 

 2    chain=Security Port Knock action=add-src-to-address-list protocol=tcp src-address-list=knock 1 address-list=knock 2 address-list-timeout=30s dst-port=1112 
      log=no log-prefix="" 

 3    chain=Security Port Knock action=reject reject-with=tcp-reset protocol=tcp src-address-list=knock 1 dst-port=1112 log=no log-prefix="" 

 4    chain=Security Port Knock action=add-src-to-address-list protocol=tcp address-list=knock 1 address-list-timeout=30s dst-port=1111 log=no log-prefix="" 

 5    chain=Security Port Knock action=reject reject-with=tcp-reset protocol=tcp dst-port=1111 log=no log-prefix=""

 6    chain=Security Port Knock action=drop log=no log-prefix="" 
```
