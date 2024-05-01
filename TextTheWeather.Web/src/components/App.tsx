import { BrowserRouter as Router} from 'react-router-dom';
import {Routes, Route} from 'react-router-dom';
import AccountPage from './AccountPage/AccountPage';
import HomePage from './HomePage/HomePage';
 
function App() {
  return (
    <Router>
      <div>
        <section>                              
            <Routes>
               <Route path="/my-account" element={<AccountPage/>}/>
               <Route path="/" element={<HomePage/>}/>
            </Routes>                    
        </section>
      </div>
    </Router>
  );
}
 
export default App;