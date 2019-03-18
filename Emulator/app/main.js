const { app, BrowserWindow } = require('electron')

function createWindow () {
  // Create the browser window.
  let win = new BrowserWindow({ width: 800, 
                                height: 600,
                                minWidth: 800, 
                                minHeight: 600,                          
                                icon: 'app/Images/HARIA.ico'})

  // and load the index.html of the app.
  win.loadFile('app/index.html')
}

app.on('ready', createWindow)