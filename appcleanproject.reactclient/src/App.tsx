import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HomePage from '@/pages/HomePage';
import AppLayout from '@/layouts/AppLayout';

function App() {

  return (
   <BrowserRouter>
    <Routes>
      <Route element={ <AppLayout /> }>
        <Route path='/' element={ <HomePage /> } />
      </Route>
    </Routes>
   </BrowserRouter>
  )
}

export default App;
