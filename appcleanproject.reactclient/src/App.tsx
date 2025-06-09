import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Toaster } from "@/components/ui/sonner";
import HomePage from '@/pages/HomePage';
import AppLayout from '@/layouts/AppLayout';
import AuthLayout from '@/layouts/AuthLayout';
import LoginPage from '@/pages/auth/LoginPage';

function App() {

  return (
   <BrowserRouter>
    <Routes>
      <Route element={ <AppLayout /> }>
        <Route path='/' element={ <HomePage /> } />
      </Route>

      <Route element={ <AuthLayout /> }>
        <Route path='/login' element={ <LoginPage /> } />
      </Route>
    </Routes>
    <Toaster />
   </BrowserRouter>
  )
}

export default App;
