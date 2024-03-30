import { createBrowserRouter, RouterProvider} from 'react-router-dom'

import Protected from './components/Protected'

import Home from './routes/Home'
import SignUp from './routes/SignUp'
import SignIn from './routes/SignIn'
import IdeaAnalysis from './routes/IdeaAnalysis'

function App() {
    const BrowserRouter = createBrowserRouter([
    { path: '/', element: <Home /> },
    { path: '/signup', element: <SignUp /> },
    { path: '/signin', element: <SignIn /> },
    { path: '/idea-analysis', element: <Protected> <IdeaAnalysis /> </Protected>},
  ])
  
  return (
    <RouterProvider router={BrowserRouter} />
  )
}

export default App
