UPDATE  T1
SET    T1. ProfileImagePath = '/MobileImage/'+T2.Name+'/'+ T1.Name+'/'+ REPLACE (T1.Name,' ','-')+'.jpg'
FROM   Mobile as T1 Left JOIN Brand as T2
ON     T1. BrandId = T2 .BrandId
