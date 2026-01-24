/*
파일명   : js/app.js
기능명   : 엔트리 포인트 - 진입점
상세     : 사용자가 버튼을 눌렀을 때, 어떤 순서로 동작할지 지시
*/

import { getFiles, saveFiles } from './api.js';
import { updateFileTable } from './ui.js';

//const btnFileUpload = document.getElementById('btn-fileupload');
const formFileUpload = document.getElementById('form-fileupload');
const inputFile = document.getElementById('input-file');

/******************************
서버에서 파일 목록 가져오기
*******************************/
async function init() {
  const files = await getFiles();
  updateFileTable(files);
}

/******************************
서버로 파일 올리기
*******************************/
formFileUpload.addEventListener('submit', async (e) => {
  e.preventDefault();

  if (!inputFile.files || inputFile.files.length === 0) return;
  if (confirm("파일을 업로드 하시겠습니까?") !== true) return;

  try {
    const result = await saveFiles(formFileUpload)

    if (result.message.length > 0) {
      alert("[File Upload!] : " + result.message);
      init();
    }
  } catch (error) {
    alert("[File Error!] : 업로드 실패!, " + error.message);
  }
});

init();