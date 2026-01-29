/*
파일명   : js/login.js
기능명   : 로그인 처리 저용
*/

import { callLoginApi } from './api.js'

const formLogin = document.getElementById('form-login');

/******************************
로그인 처리
*******************************/
formLogin.addEventListener('submit', async (e) => {
  e.preventDefault();
  try
  {
    const result = await callLoginApi(formLogin);
    if (result) {

    }
  }
  catch (error)
  {

  }
});