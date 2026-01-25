/*
파일명   : js/app.js
기능명   : 엔트리 포인트 - 진입점
상세     : 사용자가 버튼을 눌렀을 때, 어떤 순서로 동작할지 지시
*/

import { getFiles, saveFiles, downloadFileByNo } from './api.js';
import { updateFileTable } from './ui.js';

const formFileUpload = document.getElementById('form-fileupload');
const inputFile = document.getElementById('input-file');
const tblFile = document.getElementById('tbl-file');

/******************************
서버에서 파일 목록 가져오기
*******************************/
async function init() {
  try
  {
    const files = await getFiles();
    if (files) updateFileTable(files);
  }
  catch (error)
  {
    console.error("목록 초기화 중 에러 발생:", error.message);
  }
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

    if (result)
    {
      await init();
      alert("파일 업로드 성공!");
      formFileUpload.reset();
    }

  } catch (error) {
    alert("[File Error!] : 업로드 실패!, " + error.message);
  }
});

/******************************
파일을 다운로드 & Open

수정 중....
*******************************/
const downloadFile = async (e) => {
  const target = e.target;

  if (!tblFile.children) return;
  if (!target.matches('.td-filenm')) return;

  const row = target.closest('tr');
  if (!row) return;
  
  const fileNoElement = row.querySelector('.td-fileno');
  const fileNo = fileNoElement ? fileNoElement.textContent : '';
  if (!fileNo) return;

  try
  {
    //파일 다운로드 & Open
    await downloadFileByNo(fileNo);
  }
  catch (error)
  {
    alert("[File Error!] : 다운로드 실패!, " + error.message);
  }
}
tblFile.addEventListener('click', downloadFile);


/******************************
함수 호출 SECTION
*******************************/
init();