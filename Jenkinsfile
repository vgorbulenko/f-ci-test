
env.wsPath = "C:\\Jenkins_workspace\\test-pipeline"


node('master') {
    ws( env.wsPath ) {
        stage('Checkout on master') {
			checkout scm
//            checkout([$class: 'GitSCM', branches: [[name: BRANCH_NAME]], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'github-ssh', url: 'git@github.com:frustumInc/generate.git']]]) 
//          checkout([$class: 'GitSCM', branches: [[name: gitlabSourceBranch]], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'vgorbulenko_https_github', url: 'https://gitlab.amcbridge.com/spronyuk/pipeline-dev-repo']]])
		}

        stage('Checkout on master') {
			echo env.BRANCH_NAME = %env.BRANCH_NAME%
			echo env.GIT_BRANCH = %env.GIT_BRANCH%
		}
    }
}

//node('slave') {
  //  ws( env.wsPath ) {
	//    bat """ %SCRIPTS-DIR%\\sign.bat generate C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
//		bat """   %SCRIPTS-DIR%\\sign.bat msi C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""
	//	bat """   %SCRIPTS-DIR%\\sign.bat package C:\\test\\certificate\\test_sign_code_sert.pfx passw1234ord"""	
//	}
//}