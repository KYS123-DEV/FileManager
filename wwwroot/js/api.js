/*
파일명   : js/api.js
기능명   : 데이터 통신 전용
상세     : 서버 주소나 fetch 로직이 바뀌어도 이 파일만 수정하면 됨.
*/

/******************************
서버에서 파일 목록 가져오기
*******************************/
export async function getFiles() {
  const response = await fetch('/api/files');
  if (!response.ok) throw new Error("목록 조회 실패");
  return await response.json();
}

/******************************
서버로 파일 업로드 하기
*******************************/
export async function saveFiles(formElement) {
  const formData = new FormData(formElement);

  const response = await fetch('/api/files/upload', {
    method: 'POST',
    body: formData
  });

  if (!response.ok) throw new Error("파일 저장 실패");
  return await response.json();
}
