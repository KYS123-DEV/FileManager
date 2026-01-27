/*
파일명   : js/ui.js
기능명   : 화면 공용 Alert 모듈 
상세     : 사용자 화면에 띄울 Alert ui 함수 모음
*/

//******************************
// 성공 알림창
//*******************************/
export async function alertSuccess(message) {
  Swal.fire({
    title: "작업 알림",
    text: message,
    icon: "success"
  });
}