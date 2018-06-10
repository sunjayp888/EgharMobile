UPDATE  T1
SET    T1. ProfileImagePath = '/MobileImage/'+T2.Name+'/'+ T1.Name+'/'+ REPLACE (T1.Name,' ','-')+'.jpg'
FROM   Mobile as T1 Left JOIN Brand as T2
ON     T1. BrandId = T2 .BrandId
where T1.mobileId =12145
6



 Select t1.mobileId, '/MobileImage/'+T2.Name+'/'+ T1.Name+'/'+ REPLACE (T1.Name,' ','-')+'.jpg',T1.Name
FROM   Mobile as T1 Inner JOIN Brand as T2
ON     T1. BrandId = T2 .BrandId
where T1.mobileId =12145

select Profile * from mobile where mobileId =12145