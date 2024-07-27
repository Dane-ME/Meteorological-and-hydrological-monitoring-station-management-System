# Hệ thống quản lý trạm quan trắc khí tượng thủy văn
## Client app
Phần mềm khách sử dụng MAUI, biểu đồ và một vài bảng thông tin sử dụng công cụ Syncfusion để vẽ. Giao thức giao tiếp chính đưuọc sử dụng là MAUI ngoài ra còn có SMTP để gửi mail. <br /> 
<img src="./hhdJWT/Images/TopicImages.png" /> <br />
Quy ước Topic chung và riêng như ảnh trên.
Mỗi một topic tương ứng với 1 chức năng cụ thể.
- Login:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- ForgotPassword: 
  - SendMessage: UserID, VerificationCode.
- RecentSstationStatus(HomeView):
  - SendMessage: JWT.
  - ReceiveMessage: StationName, StationAddress, SeaLevel, WaveHeight, WaveLength, WaveHeightMax, WindSpeed, WindSpeedAt2mHeight, AverageWindSpeedIn2s, WindDirection, WindDirectionAt2mHeight, AverageWindDirectionIn2s.
- StationDetail:
  - SendMessage: JWT.
  - ReceiveMessage: JWT.
- UserDetail:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- ChangePassword:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- Logout:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- StationList:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- UserList:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- Stationprofile:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- Userprofile:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- (decentralization)ManagerChange:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- (decentralization)StationChange:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- RemoveUser:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.
- Regis:
  - SendMessage: UserID, EncodePass.
  - ReceiveMessage: JWT.