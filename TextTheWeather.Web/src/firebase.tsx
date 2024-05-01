// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAuth } from "firebase/auth";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyCLQBudGBvl2cZB3hGluQ8QwiAiyGC0sL8",
  authDomain: "texttheweather.firebaseapp.com",
  projectId: "texttheweather",
  storageBucket: "texttheweather.appspot.com",
  messagingSenderId: "973197662193",
  appId: "1:973197662193:web:42dab7800b5e0c32d75650"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

// Initialize Firebase Authentication and get a reference to the service
export const auth = getAuth(app);
export default app;