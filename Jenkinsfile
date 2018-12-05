
env.wsPath = "C:\\Jenkins_workspace\\test-pipeline"

if (BRANCH_NAME == "master" || BRANCH_NAME == "release") {

	node('master') {
		ws( env.wsPath ) {
			stage('Checkout on master. Branch '+BRANCH_NAME) {
				def scmVars = checkout scm
				env.GIT_URL = scmVars.GIT_URL
			}

			stage('Building stage') {
               bat 'echo Build there something============'
			}
			
		}
	}
}	


if (BRANCH_NAME == "release") {
	node ('master') {
		ws (env.wsPath) {
			stage ('Push new tag to GitHub') {
			    env.GIT_URL = env.GIT_URL.replace('https://','')
				bat 'echo prepare new release tag'
                    withCredentials([usernamePassword(credentialsId: 'vgorbulenko_https_github', passwordVariable: 'USERPASS', usernameVariable: 'USERNAME')]) {
                        bat """
							//@echo off
							set /p version=0.0.$BUILD_NUMBER
							git config --global user.email "generate-ci@frustum.io"
							git config --global user.name "Generate CI"
							git tag -a rc-%version% -m \'autotag\'
							::git push https://%USERNAME%:%USERPASS%@%GIT_URL% rc-%version%
							git push -u origin release
						"""
                    }

				
			}		
		}
	}
}


if ( BRANCH_NAME.startsWith('r-') ) {
    bat 'echo THIS IS THE TAAAAAAAAAAAAAAAAAAAAAAG!!!!!!!!!! === $BRANCH_NAME ==='
}



//node('slave') {
  //  ws( env.wsPath ) {
	//    bat """ %SCRIPTS-DIR%\\sign.bat generate C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
//		bat """   %SCRIPTS-DIR%\\sign.bat msi C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
	//	bat """   %SCRIPTS-DIR%\\sign.bat package C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""	
//	}
//}