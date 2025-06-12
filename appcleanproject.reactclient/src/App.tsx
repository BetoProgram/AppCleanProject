import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Toaster } from "@/components/ui/sonner";
import HomePage from '@/pages/HomePage';
import AppLayout from '@/layouts/AppLayout';
import AuthLayout from '@/layouts/AuthLayout';
import LoginPage from '@/pages/auth/LoginPage';
import RegisterPage from './pages/auth/RegisterPage';
import PrivateRoute from './components/PrivateRoute';

function App() {

  return (
   <BrowserRouter>
    <Routes>
      <Route element={ <PrivateRoute><AppLayout /></PrivateRoute> }>
        <Route path='/' element={ <HomePage /> } />
      </Route>

      <Route element={ <AuthLayout /> }>
        <Route path='/auth/login' element={ <LoginPage /> } />
        <Route path='/auth/register' element={<RegisterPage />} />
      </Route>
    </Routes>
    <Toaster />
   </BrowserRouter>
  )
}

export default App;
