# Hong_Solution
업무중 자주쓰는 라이브러리 모음

## Used Languages
* C#, [.NET](https://docs.microsoft.com/ko-kr/dotnet/), [OpenCVSharp](https://github.com/shimat/opencvsharp) (C#용 OpenCV)

## 목차

* **Communication**   
  다른 장치와 연결할때 사용하는 라이브러리
  
  * [Barcode](https://github.com/Willhong/Hong_Solution/blob/master/Hong_Solution/Barcode/ZebraBarcode.cs)   
  Zebra 사의 바코드리더기 통신 라이브러리 구현

  * [Camera](https://github.com/Willhong/Hong_Solution/blob/master/Hong_Solution/Camera/VisionProCam.cs)   
  대부분의 산업용 GigE 타입 카메라와 연결시킬수 있는 Cognex사의 카메라 연결 라이브러리

  * [DB-MSSQL](https://github.com/Willhong/Hong_Solution/blob/master/Hong_Solution/Communication/DB/MSSql.cs)   
  MS-SQL 데이터베이스에 데이터 읽고 쓰기

  * [Socket 통신](https://github.com/Willhong/Hong_Solution/blob/master/Hong_Solution/Communication/Socket/SocketConnect.cs)   
  소켓 통신 라이브러리 ( 연결, 데이터 받기 )

* **[Tools](https://github.com/Willhong/Hong_Solution/tree/master/Hong_Solution/Tools)**   
  자주 쓰는함수들(HongTools) 그리고 영상 처리를 위한 OpencvSharp, 상용 영상처리 라이브러리(코그넥스)

  * [HongTools](https://github.com/Willhong/Hong_Solution/tree/master/Hong_Solution/Tools/HongTools.cs)   
  컨트롤 동적할당 예시(디자이너가 아닌 코드기반 폼 생성), Json 파일 저장같이 자주쓰는 함수 추가

  * [OpenCVSharp](https://github.com/Willhong/Hong_Solution/blob/master/Hong_Solution/Tools/OpenCVClass.cs)   
  영상처리를 위한 함수들, 이진화, 블랍 찾기

  * [VisionProClass](https://github.com/Willhong/Hong_Solution/blob/master/Hong_Solution/Tools/VisionProClass.cs)   
  코그넥스사의 상용 라이브러리 사용


