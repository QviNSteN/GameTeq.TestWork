# Описание и информация

## Предысловие

Необходимо изменить названия как минимум. Доделать немного конфиги и проверить логику работы удаления файлов которые содержат больше чем одна строка.
Я всегда стараюсь делать всё более аккауратно, в том числе и .md файлы (пример хорошей вики на .md файлах от меня: https://github.com/Insomnia-IT/Wiki )
Но я в прошлый раз не закончил и т.к. сейчас не уверен, что не произойдёт предыдущей ситуации, решил, доделывать пока безсмысленно.

## Логика работы

В качестве референсов была выбрана структура работы с NoSql-базами. В качестве обучения разбирал код LiteDB. Но в итоге для собственной реализации упростил многие аспекты.
Сервисы имеют вариативность работы с множеством инстансов. Так же, для ускорения взаимодействия, имеется переключатель в воркере отправки файлов, для одного или множества инстансов.

Всё обёрнуто в докер-композ, но линк редиса не выставлен в настройках.

## Архитектура

Liberties - бибиотеки классов. Работа с файлами и работа с Redis.
LocalFilesService - всё что относится к работа с локальными файлами.
WebInputFileService - работа на приём файлов.
