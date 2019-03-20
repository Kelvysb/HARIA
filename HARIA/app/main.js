require('http')
  .createServer(require('tiny-cdn').create({}))
  .listen(8080, '0.0.0.0');

const
  electron = require('electron'),
  app = electron.app,
  BrowserWindow = electron.BrowserWindow
;

app.commandLine.appendSwitch('--ignore-gpu-blacklist');

app.once('ready', () => {

  const area = electron.screen.getPrimaryDisplay().workAreaSize;

  this.window = new BrowserWindow({
    backgroundColor: '#FFFFFF',
    frame: false,
    fullscreen: true,
    x: 0,
    y: 0,
    width: area.width,
    height: area.height
  });

  this.window.webContents.session.clearCache(function(){});

  this.window
    .once('closed', () => {
      // cleanup the reference
      this.window = null;
  })
  .loadURL('http://localhost:8080/app/index.html');

  require('fs').watch('reload', () => app.quit());

});