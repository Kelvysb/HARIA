
var gulp = require('gulp');
var sass = require('gulp-sass');
var exec = require('child_process').exec;
//var browserSync = require('browser-sync').create();

gulp.task('build', function(done) {    
    exec('npm start', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
      });
    done();
})

gulp.task('sass', function(){
    return gulp.src('app/sass/**/*.scss')
        .pipe(sass())
        .pipe(gulp.dest('app/css'));
});

gulp.task('watch', function (done){    
    gulp.watch('app/sass/**/*.scss', gulp.series('sass')); 
    /*browserSync.init({
        localOnly: true,
        files: './app'
      });
    gulp.watch("app/css/*.css").on('change', browserSync.reload);
    gulp.watch("app/js/*.js").on('change', browserSync.reload);
    gulp.watch("app/*.html").on('change', browserSync.reload);
    */
    done();
});

gulp.task('start', function(done) {
    gulp.series('build', 'watch')(done);
})