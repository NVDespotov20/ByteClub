import { createBrowserRouter, RouterProvider} from 'react-router-dom'

import Home from './routes/Home'
import SignUp from './routes/SignUp'
import SignIn from './routes/SignIn'

function App() {
    const BrowserRouter = createBrowserRouter([
    { path: '/', element: <Home /> },
    { path: '/signup', element: <SignUp /> },
    { path: '/signin', element: <SignIn /> },
  ])
  
  return (
    <RouterProvider router={BrowserRouter} />
  )
}

export default App
