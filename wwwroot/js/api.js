/*
파일명   : js/api.js
기능명   : 데이터 통신 전용
상세     : 서버 주소나 fetch 로직이 바뀌어도 이 파일만 수정하면 됨.
*/

/******************************
서버에서 파일 목록 가져오기
*******************************/
export async function getFiles() {
  try {
    const response = await fetch('/api/files');
    if (!response.ok) throw new Error("목록 조회 실패");

    return await response.json();
  }
  catch (error) {
    throw error;
  }
}

/******************************
서버로 파일 업로드 하기
*******************************/
export async function saveFiles(formElement) {
  try {
    const formData = new FormData(formElement);
    const response = await fetch('/api/files/upload', {
      method: 'POST',
      body: formData
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.message || "파일 업로드 실패!");
    }

    return await response.json();
  }
  catch (error) {
    throw error;
  }
}

/******************************
파일을 다운로드 & Open
*******************************/
export async function downloadFileByNo(fileNo) {
  try {
    const response = await fetch(`/api/files/download/${fileNo}`);

    if (!response.ok) throw new Error("파일 다운로드 실패");

    const contentDisposition = response.headers.get('Content-Disposition');
    let fileName = '';

    if (contentDisposition && contentDisposition.includes('filename=')) {
      fileName += decodeURI(contentDisposition
        .split('filename=')[1]
        .split(';')[1]
        .match(/filename\*=UTF-8''(.*)/)[1]);
    }

    const blob = await response.blob();
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();

    window.URL.revokeObjectURL(url);
    a.remove();
  }
  catch (error) {
    throw error;
  }
}